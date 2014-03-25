namespace MDM.Sync.Loaders
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;

    public class ExchangeLoader : MdmLoader<Exchange>
    {
        public ExchangeLoader(IList<Exchange> entities, bool candidateData)
            : base(entities, candidateData)
        {
        }

        protected override Exchange CreateCopyWithoutMappings(Exchange entity)
        {
            return new Exchange { Party = entity.Party, Details = entity.Details, MdmSystemData = entity.MdmSystemData };
        }
    }
}