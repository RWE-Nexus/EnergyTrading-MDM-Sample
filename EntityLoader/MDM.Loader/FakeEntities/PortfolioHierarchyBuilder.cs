namespace MDM.Loader.FakeEntities
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using MDM.Loader.NexusClient;
    using Microsoft.Practices.ServiceLocation;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Logging;
    using EnergyTrading.Mdm.Client.WebClient;
    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Search;

    public class PortfolioHierarchyBuilder 
    {
        private IMdmClient client;
        private readonly ILogger logger = LoggerFactory.GetLogger(typeof(PortfolioHierarchyBuilder));

        protected IMdmClient Client
        {
            get { return client ?? (client = ServiceLocator.Current.GetInstance<IMdmClient>()); }
        }

        public PortfolioHierarchyList Build(List<PortfolioHierarchyFake> fakes)
        {
            var portfolioHierarchys = new List<PortfolioHierarchy>();

            foreach (var fake in fakes)
            {
                logger.InfoFormat("Building Portfolio {0} of {1}", portfolioHierarchys.Count, fakes.Count);
                var portfolioHierarchy = new PortfolioHierarchy {Identifiers = fake.Identifiers};

                //child
                var childResponse = EntityFind(fake.Details.ChildPortfolio);
                if (childResponse.IsValid && childResponse.Message != null)
                {
                    MdmId nexusId = childResponse.Message.ToMdmId();

                    portfolioHierarchy.Details.ChildPortfolio = new EntityId { Identifier = nexusId };
                }

                //parent
                var parentResponse = EntityFind(fake.Details.ParentPortfolio);
                if (parentResponse.IsValid && parentResponse.Message != null)
                {
                    portfolioHierarchy.Details.ParentPortfolio = new EntityId { Identifier = parentResponse.Message.ToMdmId() }; 
                }

                portfolioHierarchy.Details.Hierarchy = new EntityId(){Identifier = new MdmId(){Identifier = "1", IsMdmId = true}, Name = "Coal"};

                portfolioHierarchys.Add(portfolioHierarchy);
            }

            var list = new PortfolioHierarchyList();
            list.AddRange(portfolioHierarchys);

            return list;
        }

        private WebResponse<Portfolio> EntityFind(Portfolio entity)
        {
            if(entity == null) return new WebResponse<Portfolio>(){ IsValid = false};

            var search = SearchBuilder.CreateSearch();
            search.AddSearchCriteria(SearchCombinator.And)
                .AddCriteria("PortfolioType", SearchCondition.Equals, entity.Details.PortfolioType, false)
                .AddCriteria("Name", SearchCondition.Equals, entity.Details.Name, false);

            var results = Client.Search<Portfolio>(search);
            if (results.IsValid)
            {
                var se = results.Message.FirstOrDefault();

                // Call again to get the ETag for the update
                return Client.Get<Portfolio>(se.ToMdmKey());
            }
            else if (results.Fault.Message.Contains("Unable to connect to the remote server")) // Try again
            {
                Thread.Sleep(30000);
                logger.WarnFormat("Try again for Portfolio: {0}-{1}", entity.Details.PortfolioType, entity.Details.Name);
                results = Client.Search<Portfolio>(search);
                if (results.IsValid)
                {
                    var se = results.Message.FirstOrDefault();

                    // Call again to get the ETag for the update
                    return Client.Get<Portfolio>(se.ToMdmKey());
                }
            }

            return new WebResponse<Portfolio>
            {
                Code = results.Code,
                IsValid = results.IsValid,
                Fault = results.Fault
            };
        }
    }
}