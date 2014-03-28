namespace MDM.Sync.Loaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Logging;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Search;

    using MDM.Loader.NexusClient;

    using Microsoft.Practices.ServiceLocation;

    public abstract class Loader
    {
        protected EventHandler loadCompleted = delegate { };

        private readonly ILogger logger = LoggerFactory.GetLogger(typeof(Loader));

        private IMdmClient client;

        public event EventHandler LoadCompleted
        {
            add
            {
                loadCompleted += value;
            }

            remove
            {
                loadCompleted -= value;
            }
        }

        protected IMdmClient Client
        {
            get
            {
                return client ?? (client = ServiceLocator.Current.GetInstance<IMdmClient>());
            }
        }

        public void Load()
        {
            try
            {
                this.OnLoad();
                this.loadCompleted(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        protected T CreateUpdate<T>(
            string name, 
            Func<WebResponse<T>> creator, 
            Func<WebResponse<T>, WebResponse<T>> updater, 
            string version = null, 
            int retries = 3) where T : class, IMdmEntity
        {
            return CreateUpdate(() => GetByName<T>(name), creator, updater, version, retries);
        }

        protected T CreateUpdate<T>(
            Func<WebResponse<T>> finder, 
            Func<WebResponse<T>> creator, 
            Func<WebResponse<T>, WebResponse<T>> updater, 
            string version = null, 
            int retries = 3) where T : class
        {
            var response = Try(() => this.CreateUpdate(finder, creator, updater, version), retries);

            return response.IsValid ? response.Message : null;
        }

        protected WebResponse<T> CreateUpdate<T>(
            Func<WebResponse<T>> finder, 
            Func<WebResponse<T>> creator, 
            Func<WebResponse<T>, WebResponse<T>> updater, 
            string version = null)
        {
            var response = finder.Invoke();
            this.LogWebResponse("finder", response);

            switch (response.Code)
            {
                case HttpStatusCode.OK:
                    if (!string.IsNullOrWhiteSpace(version) && response.Tag != version)
                    {
                        throw new MdmLoadConcurrencyException(
                            "Different Entity versions - supplied version = " + version + " and Mdm Version = "
                            + response.Tag);
                    }

                    response = updater.Invoke(response);
                    this.LogWebResponse("update", response);
                    break;

                case HttpStatusCode.NotFound:
                    response = creator.Invoke();
                    this.LogWebResponse("create", response);
                    break;
            }

            return response;
        }

        protected WebResponse<TContract> EditSearch<TContract>(Search search) where TContract : class, IMdmEntity
        {
            var results = this.SingletonSearch<TContract>(search);
            if (results.IsValid)
            {
                var se = results.Message;

                // Call again to get the ETag for the update
                return Client.Get<TContract>(se.ToMdmKey());
            }

            return results;
        }

        protected WebResponse<T> FindCreate<T>(Func<WebResponse<T>> finder, Func<WebResponse<T>> creator)
        {
            var response = finder.Invoke();
            this.LogWebResponse("finder", response);

            if (response.Code == HttpStatusCode.NotFound)
            {
                response = creator.Invoke();
                this.LogWebResponse("create", response);
            }

            return response;
        }

        /// <summary>
        /// Invokes multiple finders until the first success or error (excluding NotFound)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="finders"></param>
        /// <returns></returns>
        protected WebResponse<T> FinderChain<T>(params Func<WebResponse<T>>[] finders)
        {
            foreach (var finder in finders)
            {
                var response = finder.Invoke();
                if (response.Code == HttpStatusCode.NotFound)
                {
                    continue;
                }

                return response;
            }

            return new WebResponse<T> { Code = HttpStatusCode.NotFound, IsValid = false };
        }

        protected WebResponse<TContract> GetByName<TContract>(string name) where TContract : class, IMdmEntity
        {
            var search = NameSearch(name);
            var entity = this.EditSearch<TContract>(search);
            if (entity.IsValid)
            {
                logger.Debug(typeof(TContract).Name + ": Found existing named " + name);
            }

            return entity;
        }

        protected T GetCreate<T>(string name, Func<WebResponse<T>> creator, int retries = 3) where T : class, IMdmEntity
        {
            return GetCreate(() => GetByName<T>(name), creator, retries);
        }

        protected T GetCreate<T>(Func<WebResponse<T>> finder, Func<WebResponse<T>> creator, int retries = 3)
            where T : class
        {
            var response = Try(() => this.FindCreate(finder, creator), retries);

            return response.IsValid ? response.Message : null;
        }

        /// <summary>
        /// Unifies get and create for a particular mapping
        /// assumes that DefaultReverse is never set supplied for use with Trayport Loader
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="id"></param>
        /// <param name="system">SystemName of mapping to update</param>
        /// <param name="identifier">Identifier of mapping to update</param>
        /// <returns></returns>
        protected MdmId GetOrCreateMapping<TContract>(int id, string system, string identifier)
            where TContract : class, IMdmEntity
        {
            return GetOrCreateMapping<TContract>(id, new MdmId { SystemName = system, Identifier = identifier });
        }

        /// <summary>
        /// Unifies get and create for a particular mapping
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="id"></param>
        /// <param name="mapping">mapping to update</param>
        /// <returns></returns>
        protected MdmId GetOrCreateMapping<TContract>(int id, MdmId mapping) where TContract : class, IMdmEntity
        {
            return GetCreate(
                () => this.GetOrDeleteMappingWithDefaultReverseCheck<TContract>(id, mapping), 
                () =>
                Client.CreateMapping<TContract>(
                    id, 
                    mapping.SystemName, 
                    mapping.Identifier, 
                    mapping.DefaultReverseIndSpecified ? mapping.DefaultReverseInd.Value : false));
        }

        /// <summary>
        /// Create a search to locate an entity by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected Search NameSearch(string name)
        {
            return SearchFactory.SimpleSearch("Name", SearchCondition.Equals, name);
        }

        protected abstract void OnLoad();

        protected T Retry<T>(Func<T> func, int retries = 3) where T : class
        {
            T result = null;
            var i = 0;
            while (result == null && i++ < retries)
            {
                result = func.Invoke();

                if (result == null)
                {
                    Thread.Sleep(100 + i * 200);
                }
            }

            return result;
        }

        protected WebResponse<IList<T>> Search<T>(Search search) where T : IMdmEntity
        {
            return Client.Search<T>(search);
        }

        protected WebResponse<TContract> SingletonSearch<TContract>(Search search) where TContract : class, IMdmEntity
        {
            var results = Client.Search<TContract>(search);
            if (results.IsValid)
            {
                return new WebResponse<TContract>
                           {
                               Code =
                                   results.Message.Count() > 0
                                       ? HttpStatusCode.OK
                                       : HttpStatusCode.NotFound, 
                               Message = results.Message.FirstOrDefault()
                           };
            }

            return new WebResponse<TContract> { Code = results.Code, IsValid = results.IsValid, Fault = results.Fault };
        }

        protected WebResponse<T> Try<T>(Func<WebResponse<T>> func, int retries)
        {
            var response = new WebResponse<T> { IsValid = false };
            var i = 0;
            while (!response.IsValid && i++ < retries)
            {
                response = func.Invoke();
                if (!response.IsValid)
                {
                    var message = response.Fault == null ? "Unknown" : response.Fault.Message.TrimNewLinesAndTabs();
                    logger.Warn("Try_" + typeof(T).Name + ": " + response.Code + " - " + message);
                    Thread.Sleep(100);
                }
            }

            return response;
        }

        private WebResponse<MdmId> GetOrDeleteMappingWithDefaultReverseCheck<TContract>(int id, MdmId mapping)
            where TContract : class, IMdmEntity
        {
            var response = Client.GetMapping<TContract>(id, mapping.SystemName, mapping.Identifier);
            if (response.IsValid && response.Message.DefaultReverseInd != mapping.DefaultReverseInd)
            {
                if (response.Message.MappingId.HasValue)
                {
                    Client.DeleteMapping<TContract>(id, (int)response.Message.MappingId.Value);
                    return new WebResponse<MdmId> { Code = HttpStatusCode.NotFound };
                }
            }

            return response;
        }

        private void LogWebResponse<T>(string operation, WebResponse<T> response)
        {
            switch (response.Code)
            {
                case HttpStatusCode.HttpVersionNotSupported:
                case HttpStatusCode.GatewayTimeout:
                case HttpStatusCode.LengthRequired:
                case HttpStatusCode.PreconditionFailed:
                case HttpStatusCode.RequestEntityTooLarge:
                case HttpStatusCode.RequestUriTooLong:
                case HttpStatusCode.UnsupportedMediaType:
                case HttpStatusCode.RequestedRangeNotSatisfiable:
                case HttpStatusCode.ExpectationFailed:
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.NotImplemented:
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.MethodNotAllowed:
                case HttpStatusCode.NotAcceptable:
                case HttpStatusCode.ProxyAuthenticationRequired:
                case HttpStatusCode.RequestTimeout:
                    this.logger.ErrorFormat(
                        "{0} {1} web response: IsValid:{2}; HttpCode:{3}; FaultMessage:{4}", 
                        typeof(T).Name, 
                        operation, 
                        response.IsValid.ToString(), 
                        response.Code.ToString(), 
                        response.Fault == null ? string.Empty : response.Fault.Message.TrimNewLinesAndTabs());
                    break;

                default:
                    this.logger.DebugFormat(
                        "{0} {1} web response: IsValid:{2}; HttpCode:{3}; FaultMessage:{4}", 
                        typeof(T).Name, 
                        operation, 
                        response.IsValid.ToString(), 
                        response.Code.ToString(), 
                        response.Fault == null ? string.Empty : response.Fault.Message.TrimNewLinesAndTabs());
                    break;
            }
        }
    }
}