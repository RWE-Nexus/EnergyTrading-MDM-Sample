namespace MDM.Sync.Loaders
{
    using System;
    using System.Collections.Generic;

    using EnergyTrading.Logging;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Mdm.Contracts;

    using OpenNexus.MDM.Contracts;

    public class MdmLoader<T> : Loader
        where T : class, IMdmEntity, new()
    {
        private readonly bool candidateData;

        private readonly string entityName = string.Empty;

        private readonly ILogger logger = LoggerFactory.GetLogger(typeof(MdmLoader<T>));

        public MdmLoader()
        {
        }

        public MdmLoader(IList<T> entities, bool candidateData)
        {
            this.candidateData = candidateData;
            this.Entities = entities;
            this.PrimarySystem = "Spreadsheet";
            this.NexusNameCache = new Dictionary<string, string>();
            this.entityName = typeof(T).Name;
        }

        public IList<T> Entities { get; set; }

        /// <summary>
        /// Gets or sets the system we use for the primary identifier.
        /// </summary>
        public string PrimarySystem { get; set; }

        protected Dictionary<string, string> NexusNameCache { get; private set; }

        public T Load(T entity)
        {
            return Load(entity, null);
        }

        public T Load(T entity, string entityVersion)
        {
            var copy = CreateCopyWithoutMappings(entity);

            // Any changes we need to the data before create/update
            var ok = this.BeforeCreateUpdate(copy);
            if (!ok)
            {
                this.logger.ErrorFormat(
                    "Unable to create or update {0} due to un-available dependent entities. Please check the logs and try again.", 
                    entityName);
                return null;
            }

            T entityAfterOperation = null;
            try
            {
                if (this.candidateData)
                {
                    entityAfterOperation = GetCreate(() => this.Find(entity), () => Client.Create(copy));
                }
                else
                {
                    entityAfterOperation = CreateUpdate(
                        () => this.Find(entity), 
                        () => Client.Create(copy), 
                        w => Update(w, copy), 
                        entityVersion, 
                        3);
                }
            }
            catch (MdmLoadConcurrencyException concurrencyException)
            {
                this.logger.InfoFormat(
                    "Concurrency Exception while creating / updating {0}: {1}. Returning error notification", 
                    this.entityName, 
                    concurrencyException.Message);
                throw;
            }
            catch (Exception ex)
            {
                this.logger.ErrorFormat(
                    "Error occurred whilst creating / updating {0}: {1}, InnerException: {2}", 
                    this.entityName, 
                    ex.Message, 
                    ex.InnerException == null ? string.Empty : ex.InnerException.Message);
            }

            if (entityAfterOperation == null)
            {
                this.logger.WarnFormat("{0} has not been returned after create / update operation.", this.entityName);
                return null;
            }

            // Sanity check for loaded entities
            if (entity.Identifiers == null)
            {
                this.logger.DebugFormat("{0} doen't have Identifiers", this.entityName);
                return null;
            }

            foreach (var mapping in entity.Identifiers)
            {
                this.GetOrCreateMapping<T>(entityAfterOperation.ToMdmKey(), mapping);
            }

            // get a fresh copy of the entity to obtain all the mappings including the new ones we sent in
            // use a try catch in case Find throws an exception
            try
            {
                var response = this.Find(entity);
                if (response.IsValid)
                {
                    return response.Message;
                }

                this.logger.Error(
                    "Error occurred whilst obtaining latest entity for return. returned entity may not have all mappings");
            }
            catch (Exception ex)
            {
                this.logger.ErrorFormat(
                    "Error occurred whilst obtaining latest entity for return. returned entity may not have all mappings  {0}: {1}, InnerException: {2}", 
                    this.entityName, 
                    ex.Message, 
                    ex.InnerException == null ? string.Empty : ex.InnerException.Message);
            }

            return entityAfterOperation;
        }

        /// <summary>
        /// Fix up an entity before we can use it for create/update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>true if we sucessfully did our work, false otherwise</returns>
        protected virtual bool BeforeCreateUpdate(T entity)
        {
            return true;
        }

        protected bool ConvertNexusNameToId<TEntity>(EntityId entityId) where TEntity : class, IMdmEntity
        {
            return ConvertNexusNameToId<TEntity>(entityId, false);
        }

        protected bool ConvertNexusNameToIdOptional<TEntity>(EntityId entityId) where TEntity : class, IMdmEntity
        {
            return ConvertNexusNameToId<TEntity>(entityId, true);
        }

        protected virtual T CreateCopyWithoutMappings(T entity)
        {
            return new T { Details = entity.Details, MdmSystemData = entity.MdmSystemData };
        }

        protected virtual WebResponse<T> EntityFind(T entity)
        {
            var search = NameSearch(this.EntityName(entity));

            return this.EditSearch<T>(search);
        }

        protected WebResponse<T> Find(T entity)
        {
            var id = entity.Identifiers.PrimaryIdentifier(PrimarySystem);
            var response = id != null
                               ? this.FinderChain(() => this.Client.Get<T>(id), () => EntityFind(entity))
                               : this.EntityFind(entity);

            return response;
        }

        protected override void OnLoad()
        {
            foreach (var entity in this.Entities)
            {
                logger.DebugFormat("{0}: Begin load", this.entityName);
                this.Load(entity);
                logger.DebugFormat("{0}: Load complete", entityName);
            }
        }

        /// <summary>
        /// Update the entity based on its nexus id
        /// </summary>
        /// <param name="response"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected WebResponse<T> Update(WebResponse<T> response, T entity)
        {
            var id = response.Message.ToMdmKey();
            return Client.Update(id, entity, response.Tag);
        }

        /// <summary>
        /// Checks a EntityId to see if we are a Nexus reference and
        /// if so fills in the identifier using a name lookup
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="isOptional"></param>
        private bool ConvertNexusNameToId<TEntity>(EntityId entityId, bool isOptional) where TEntity : class, IMdmEntity
        {
            if (entityId == null)
            {
                return isOptional;
            }

            if (entityId.Identifier != null)
            {
                if (!entityId.Identifier.IsMdmId)
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(entityId.Identifier.Identifier))
                {
                    return true;
                }
            }

            if (string.IsNullOrEmpty(entityId.Name))
            {
                return isOptional;
            }

            if (entityId.Identifier == null)
            {
                entityId.Identifier = new MdmId { SystemName = SystemNames.Nexus, IsMdmId = true };
            }

            string cacheKey = string.Format("{0}|{1}", typeof(TEntity), entityId.Name);
            if (NexusNameCache.ContainsKey(cacheKey))
            {
                entityId.Identifier.Identifier = NexusNameCache[cacheKey];
                logger.DebugFormat(
                    "Found entity with name {0} in cache, using Identifier {1}", 
                    cacheKey, 
                    NexusNameCache[cacheKey]);
                return true;
            }

            var search = NameSearch(entityId.Name);

            var response = this.EditSearch<TEntity>(search);
            if (!response.IsValid)
            {
                logger.ErrorFormat("{0}: Cannot locate entity with name {1}", typeof(TEntity).Name, entityId.Name);
                return false;
            }

            entityId.Identifier.Identifier = response.Message.ToMdmKey().ToString();
            NexusNameCache.Add(cacheKey, entityId.Identifier.Identifier);
            return true;
        }

        private string EntityName(T entity)
        {
            switch (typeof(T).Name)
            {
                case "Agreement":
                    var agreementDetails = (AgreementDetails)entity.Details;
                    return agreementDetails.Name;
                case "Book":
                    var bookDetails = (BookDetails)entity.Details;
                    return bookDetails.Name;
                case "BookDefault":
                    var bookdefaultDetails = (BookDefaultDetails)entity.Details;
                    return bookdefaultDetails.Name;
                case "Broker":
                    var broker = (BrokerDetails)entity.Details;
                    return broker.Name;

                case "BrokerCommodity":
                    var brokerCommodity = (BrokerCommodityDetails)entity.Details;
                    return brokerCommodity.Name;

                case "BusinessUnit":
                    var bu = (BusinessUnitDetails)entity.Details;
                    return bu.Name;

                case "Calendar":
                    var cd = (CalendarDetails)entity.Details;
                    return cd.Name;

                case "Commodity":
                    var cmd = (CommodityDetails)entity.Details;
                    return cmd.Name;

                case "Counterparty":
                    var cpty = (CounterpartyDetails)entity.Details;
                    return cpty.Name;

                case "Curve":
                    var cv = (CurveDetails)entity.Details;
                    return cv.Name;

                case "Exchange":
                    var exchange = (ExchangeDetails)entity.Details;
                    return exchange.Name;

                case "InstrumentType":
                    var it = (InstrumentTypeDetails)entity.Details;
                    return it.Name;

                case "InstrumentTypeOverride":
                    var ito = (InstrumentTypeOverrideDetails)entity.Details;
                    return ito.Name;

                case "LegalEntity":
                    var legalEntity = (LegalEntityDetails)entity.Details;
                    return legalEntity.Name;

                case "Market":
                    var md = (MarketDetails)entity.Details;
                    return md.Name;

                case "Person":
                    var personDetails = (PersonDetails)entity.Details;
                    return personDetails.Email;

                case "Party":
                    var party = (PartyDetails)entity.Details;
                    return party.Name;

                case "PartyAccountability":
                    var partyAccountability = (PartyAccountabilityDetails)entity.Details;
                    return partyAccountability.Name;

                case "PartyRoleAccountability":
                    var partyRoleAccoutability = (PartyRoleAccountabilityDetails)entity.Details;
                    return partyRoleAccoutability.Name;

                case "PartyRole":
                    var partyRole = (PartyRoleDetails)entity.Details;
                    return partyRole.Name;

                case "Product":
                    var product = (ProductDetails)entity.Details;
                    return product.Name;

                case "ProductCurve":
                    var productCurve = (ProductCurveDetails)entity.Details;
                    return productCurve.Name;

                case "ProductScota":
                    var productScota = (ProductScotaDetails)entity.Details;
                    return productScota.Name;

                case "ProductType":
                    var productType = (ProductTypeDetails)entity.Details;
                    return productType.Name;

                case "SettlementContact":
                    var scd = (SettlementContactDetails)entity.Details;
                    return scd.Name;

                case "SourceSystem":
                    var ss = (SourceSystemDetails)entity.Details;
                    return ss.Name;

                case "Vessel":
                    var vessel = (VesselDetails)entity.Details;
                    return vessel.Name;

                case "Unit":
                    var ud = (UnitDetails)entity.Details;
                    return ud.Name;

                case "Tenor":
                    var tenor = (TenorDetails)entity.Details;
                    return tenor.Name;

                case "TenorType":
                    var tenorType = (TenorTypeDetails)entity.Details;
                    return tenorType.Name;

                default:
                    throw new NotSupportedException("No EntityName block in case statement for " + typeof(T).Name);
            }
        }
    }
}