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

    public class PersonLoader : MdmLoader<Person>
    {
        private readonly ILogger logger = LoggerFactory.GetLogger<PartyRoleLoader>();

        public PersonLoader(IList<Person> entities, bool candidateData)
            : base(entities, candidateData)
        {
        }

        protected override WebResponse<Person> EntityFind(Person entity)
        {
            var commonId = entity.Identifiers.PrimaryIdentifier("Common");
            return this.FinderChain(() => this.Client.Get<Person>(commonId), () => this.NameSearch(entity));
        }

        private WebResponse<Person> NameSearch(Person entity)
        {
            var search = SearchBuilder.CreateSearch();
            search.AddSearchCriteria(SearchCombinator.And)
                .AddCriteria("FirstName", SearchCondition.Equals, entity.Details.Forename)
                .AddCriteria("LastName", SearchCondition.Equals, entity.Details.Surname);

            var results = Client.Search<Person>(search);

            if (results.IsValid)
            {
                var se = results.Message.FirstOrDefault();

                // Call again to get the ETag for the update
                return Client.Get<Person>(se.ToMdmKey());
            }

            return new WebResponse<Person> { Code = results.Code, IsValid = results.IsValid, Fault = results.Fault };
        }
    }
}