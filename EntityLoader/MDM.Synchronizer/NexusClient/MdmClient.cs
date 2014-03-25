namespace MDM.Loader.NexusClient
{
    using System.Collections.Generic;

    using MDM.Sync;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Logging;
    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.Mdm.Client.WebClient;
    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;

    public class MdmClient : IMdmClient
    {
        private readonly IMdmService service;
        private readonly ILogger logger = LoggerFactory.GetLogger(typeof(MdmClient));
        

        public MdmClient(IMdmService service)
        {
            this.service = service;
        }

        public WebResponse<TContract> Create<TContract>(TContract contract)
            where TContract : IMdmEntity
        {
            logger.DebugFormat("{0}: Create request sent", typeof(TContract).Name);
            return service.Create(contract);
        }

        public WebResponse<TContract> Get<TContract>(int id)
            where TContract : IMdmEntity
        {
            logger.DebugFormat("{0}: Looking for {1}", typeof(TContract).Name, id.ToString());
            return service.Get<TContract>(id);
        }

        public WebResponse<TContract> Get<TContract>(MdmId identifier)
            where TContract : IMdmEntity
        {
            logger.DebugFormat("{0}: Looking for {1}", typeof(TContract).Name, identifier);
            return service.Get<TContract>(identifier);  
        }

        public WebResponse<IList<TContract>> Search<TContract>(Search search)
            where TContract : IMdmEntity
        {
            logger.DebugFormat("{0}: Search request sent", typeof(TContract).Name);
            return service.Search<TContract>(search);
        }

        public WebResponse<MdmId> CreateMapping<TContract>(int id, string systemName, string mappingString, bool defaultSystemMapping = false)
            where TContract : IMdmEntity
        {
            logger.DebugFormat("{0}: Creating Mapping for {1} - {2}/{3}", typeof(TContract).Name, id, systemName, mappingString);
            var identifier = new MdmId
            {
                SystemName = systemName,
                Identifier = mappingString,
                DefaultReverseInd = defaultSystemMapping,
            };

            var result = service.CreateMapping<TContract>(id, identifier);
            if(!result.IsValid)
            {
                var message = string.Format("{0} - {1}:{2}",
                    result.Message,
                    result.Fault != null ? result.Fault.Reason.TrimNewLinesAndTabs() : string.Empty,
                    result.Fault != null ? result.Fault.Message.TrimNewLinesAndTabs() : string.Empty);

                logger.ErrorFormat("{0}: Unable to create mapping for {1} - {2}/{3}: Message: {4}", typeof(TContract).Name, id, systemName, mappingString, message);
            }

            return result;
        }

        public WebResponse<MdmId> GetMapping<TContract>(int id, string systemName, string mappingString)
            where TContract : IMdmEntity
        {
            var result = service.GetMapping<TContract>(id, x => x.SystemName == systemName && x.Identifier == mappingString);
            if (result.IsValid)
            {
                logger.DebugFormat("{0}: Mapping found {1}/{2}", typeof(TContract).Name, systemName, mappingString);
            }

            return result;
        }

        /// <summary>
        /// Update an entity including etag for checking the update is still valid
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <param name="etag"></param>
        /// <returns></returns>
        public WebResponse<TContract> Update<TContract>(int id, TContract entity, string etag)
            where TContract : IMdmEntity
        {
            var result = service.Update(id, entity, etag);
            if (result.IsValid)
            {
                logger.DebugFormat("{0}: Updated {1}", typeof(TContract).Name, id);
            }

            return result;
        }

        /// <summary>
        /// Delete the mapping from the entity (for cases where an item other than systemName or Identifier is altered
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="id"></param>
        /// <param name="mappingId"></param>
        /// <returns></returns>
        public WebResponse<TContract> DeleteMapping<TContract>(int id, int mappingId) where TContract : IMdmEntity
        {
            var result = service.DeleteMapping<TContract>(id, mappingId);
            if (result.IsValid)
            {
                logger.DebugFormat("{0}: Mapping Deleted {1}", typeof(TContract).Name, mappingId);
            }
            return result;
        }
    }
}