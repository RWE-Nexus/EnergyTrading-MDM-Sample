namespace MDM.Sync.Loaders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Logging;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Search;

    using OpenNexus.MDM.Contracts;

    public class PartyRoleLoader : MdmLoader<PartyRole>
    {
        private readonly ILogger logger = LoggerFactory.GetLogger<PartyRoleLoader>();

        public PartyRoleLoader(IList<PartyRole> entities, bool candidateData)
            : base(entities, candidateData)
        {
        }

        protected override PartyRole CreateCopyWithoutMappings(PartyRole entity)
        {
            return new PartyRole
                       {
                           Details = entity.Details, 
                           MdmSystemData = entity.MdmSystemData, 
                           PartyRoleType = entity.PartyRoleType, 
                           Party = entity.Party
                       };
        }

        protected override WebResponse<PartyRole> EntityFind(PartyRole entity)
        {
            var search = SearchBuilder.CreateSearch();
            var searchCriteria = search.AddSearchCriteria(SearchCombinator.And)
                .AddCriteria("Name", SearchCondition.Equals, entity.Details.Name);

            if (!string.IsNullOrWhiteSpace(entity.PartyRoleType))
            {
                searchCriteria.AddCriteria("PartyRole.PartyRoleType", SearchCondition.Equals, entity.PartyRoleType);
            }

            var results = Client.Search<PartyRole>(search);

            if (results.IsValid)
            {
                var se = results.Message.FirstOrDefault();

                // Call again to get the ETag for the update
                return Client.Get<PartyRole>(se.ToMdmKey());
            }

            if (results.Fault != null && results.Fault.Message.Contains("Unable to connect to the remote server"))
            {
                // Try again
                Thread.Sleep(30000);
                this.logger.WarnFormat("Try again for PartyRole: {0}-{1}", entity.PartyRoleType, entity.Details.Name);
                results = this.Client.Search<PartyRole>(search);
                if (results.IsValid)
                {
                    var se = results.Message.FirstOrDefault();

                    // Call again to get the ETag for the update
                    return this.Client.Get<PartyRole>(se.ToMdmKey());
                }
            }

            return new WebResponse<PartyRole> { Code = results.Code, IsValid = results.IsValid, Fault = results.Fault };
        }
    }
}