namespace MDM.Sync.Loaders
{
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Logging;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Search;

    using OpenNexus.MDM.Contracts;

    public class PartyLoader : MdmLoader<Party>
    {
        private readonly ILogger logger = LoggerFactory.GetLogger<PartyRoleLoader>();

        public PartyLoader(bool candidateData)
            : this(new List<Party>(), candidateData)
        {
        }

        public PartyLoader(IList<Party> entities, bool candidateData)
            : base(entities, candidateData)
        {
        }

        protected override WebResponse<Party> EntityFind(Party entity)
        {
            var search = SearchBuilder.CreateSearch();

            var searchCriteria = search.AddSearchCriteria(SearchCombinator.And)
                .AddCriteria("Name", SearchCondition.Equals, entity.Details.Name);

            if (!string.IsNullOrWhiteSpace(entity.Details.Role))
            {
                searchCriteria.AddCriteria("Role", SearchCondition.Equals, entity.Details.Role);
            }

            var results = Client.Search<Party>(search);

            if (results.IsValid)
            {
                var se = results.Message.FirstOrDefault();

                // Call again to get the ETag for the update
                return Client.Get<Party>(se.ToMdmKey());
            }

            return new WebResponse<Party> { Code = results.Code, IsValid = results.IsValid, Fault = results.Fault };
        }
    }
}