using System.Collections.Generic;
using System.Linq;
using EnergyTrading.Contracts.Search;
using EnergyTrading.Mdm.Client.WebClient;
using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;
using EnergyTrading.Search;

namespace MDM.Sync.Loaders
{
    public class LocationLoader : MdmLoader<Location>
    {
        public LocationLoader(IList<Location> entities, bool candidateData)
            : base(entities, candidateData)
        {
        }

        protected override WebResponse<Location> EntityFind(Location entity)
        {
           
            var search = SearchBuilder.CreateSearch();
            search.AddSearchCriteria(SearchCombinator.And)
                .AddCriteria("Type", SearchCondition.Equals, entity.Details.Type, isNumeric:false)
                .AddCriteria("Name", SearchCondition.Equals, entity.Details.Name, isNumeric:false);

            var results = Client.Search<Location>(search);
            if (results.IsValid)
            {
                var se = results.Message.FirstOrDefault();

                // Call again to get the ETag for the update
                return Client.Get<Location>(se.ToMdmKey());
            }

            return new WebResponse<Location>
            {
                Code = results.Code,
                IsValid = results.IsValid,
                Fault = results.Fault
            };
        }
    }
}