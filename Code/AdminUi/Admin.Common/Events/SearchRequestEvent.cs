namespace Common.Events
{
    using EnergyTrading.Contracts.Search;

    public class SearchRequestEvent
    {
        public SearchRequestEvent(Search search, string entityName)
        {
            this.EntityName = entityName;
            this.Search = search;
        }

        public string EntityName { get; set; }

        public Search Search { get; private set; }
    }
}