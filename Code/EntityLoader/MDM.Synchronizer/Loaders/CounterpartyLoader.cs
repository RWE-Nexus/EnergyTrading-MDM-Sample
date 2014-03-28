namespace MDM.Sync.Loaders
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;

    public class CounterpartyLoader : MdmLoader<Counterparty>
    {
        public CounterpartyLoader(IList<Counterparty> entities, bool candidateData)
            : base(entities, candidateData)
        {
        }

        protected override Counterparty CreateCopyWithoutMappings(Counterparty entity)
        {
            return new Counterparty
                       {
                           Party = entity.Party, 
                           Details = entity.Details, 
                           MdmSystemData = entity.MdmSystemData
                       };
        }
    }
}