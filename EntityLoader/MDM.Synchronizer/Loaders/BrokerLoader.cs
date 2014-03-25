namespace MDM.Sync.Loaders
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;

    public class BrokerLoader : MdmLoader<Broker>
    {
        public BrokerLoader(IList<Broker> entities, bool candidateData)
            : base(entities, candidateData)
        {
        }

        protected override Broker CreateCopyWithoutMappings(Broker entity)
        {
            return new Broker { Party = entity.Party, Details = entity.Details, MdmSystemData = entity.MdmSystemData };
        }
    }
}