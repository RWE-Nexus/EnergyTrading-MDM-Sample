namespace MDM.Sync.Loaders
{
    using System.Collections.Generic;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Search;

    using OpenNexus.MDM.Contracts;

    public class LegalEntityLoader : MdmLoader<LegalEntity>
    {
        public LegalEntityLoader()
        {
        }

        public LegalEntityLoader(IList<LegalEntity> entities, bool candidateData)
            : base(entities, candidateData)
        {
        }

        protected override LegalEntity CreateCopyWithoutMappings(LegalEntity entity)
        {
            return new LegalEntity
                       {
                           Details = entity.Details, 
                           MdmSystemData = entity.MdmSystemData, 
                           Party = entity.Party
                       };
        }

        protected override WebResponse<LegalEntity> EntityFind(LegalEntity entity)
        {
            var search = SearchFactory.SimpleSearch(
                "RegisteredName", 
                SearchCondition.Equals, 
                entity.Details.RegisteredName);
            return this.EditSearch<LegalEntity>(search);
        }
    }
}