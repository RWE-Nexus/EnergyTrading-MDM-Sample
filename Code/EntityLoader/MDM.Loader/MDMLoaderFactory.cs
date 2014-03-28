namespace MDM.Loader
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using EnergyTrading.Logging;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Xml.Serialization;

    using MDM.Loader.FakeEntities;
    using MDM.Sync.Loaders;

    using OpenNexus.MDM.Contracts;

    public class MDMLoaderFactory : ICreateMDMLoader
    {
        private readonly ILogger logger = LoggerFactory.GetLogger(typeof(MDMLoaderFactory));

        public Loader Create(string entityName, string entitiesXmlfileName, bool candidateData)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                this.logger.Error("Entity name is either null or empty string.");
                return null;
            }

            if (string.IsNullOrWhiteSpace(entitiesXmlfileName) || !File.Exists(entitiesXmlfileName))
            {
                this.logger.ErrorFormat("Invalid entities xml, file path - {0}.", entitiesXmlfileName);
                return null;
            }

            switch (entityName.Trim().ToLower())
            {
                case "broker":
                    return Create<BrokerList>(
                        entitiesXmlfileName, 
                        entities => new BrokerLoader(entities, candidateData));

                case "counterparty":
                    return Create<CounterpartyList>(
                        entitiesXmlfileName, 
                        entities => new CounterpartyLoader(entities, candidateData));

                case "exchange":
                    return Create<ExchangeList>(
                        entitiesXmlfileName, 
                        entities => new ExchangeLoader(entities, candidateData));

                case "legalentity":
                    return Create<LegalEntityList>(
                        entitiesXmlfileName, 
                        entities => new LegalEntityLoader(entities, candidateData));

                case "location":
                    return Create<LocationList>(
                        entitiesXmlfileName, 
                        entities => new LocationLoader(entities, candidateData));

                case "party":
                    return Create<PartyList>(entitiesXmlfileName, entities => new PartyLoader(entities, candidateData));

                case "partyrole":
                    return Create<PartyRoleList>(
                        entitiesXmlfileName, 
                        entities => new PartyRoleLoader(entities, candidateData));

                case "person":
                    return Create<PersonList>(
                        entitiesXmlfileName, 
                        entitites => new PersonLoader(entitites, candidateData));

                case "referencedata":
                    return Create<ReferenceDataFakeList>(
                        entitiesXmlfileName, 
                        entities => new ReferenceDataLoader(new ReferenceDataBuilder().Build(entities)));

                case "sourcesystem":
                    return Create<SourceSystem, SourceSystemList>(entitiesXmlfileName, candidateData);

                default:
                    throw new NotImplementedException(
                        string.Format("Loader has not been implemented for {0} entity.", entityName));
            }
        }

        private Loader Create<TEntity, TList>(string fileName, bool candidateData)
            where TEntity : class, IMdmEntity, new() where TList : class, IList<TEntity>
        {
            return Create<TList>(fileName, entities => new MdmLoader<TEntity>(entities, candidateData));
        }

        private Loader Create<TList>(string fileName, Func<TList, Loader> func) where TList : class
        {
            try
            {
                var entities = fileName.DeserializeDataContractXml<TList>();
                return func(entities);
            }
            catch (Exception ex)
            {
                this.logger.ErrorFormat(
                    "Exception occurred whilst deserializing the entities xml file and creating the loader: {0}; InnerException: {1}", 
                    ex.Message, 
                    ex.InnerException);
                return null;
            }
        }
    }
}