namespace EnergyTrading.MDM.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Syndication;
    using System.Xml;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Search;
    using EnergyTrading.Mdm.Contracts;

    [TestFixture]
    public class when_a_search_for_a_sourcesystem_is_made_with_a_specific_name_and_results_are_found : IntegrationTestBase
    {
        private static HttpClient client;

        private static HttpContent content;

        private static MDM.SourceSystem entity1;

        private static MDM.SourceSystem entity2;

        private static HttpResponseMessage response;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        [Test]
        public void should_return_the_ok_status_code()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void should_return_the_relevant_search_results()
        {
            XmlReader reader = XmlReader.Create(
                response.Content.ReadAsStream(), new XmlReaderSettings { ProhibitDtd = false });
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            List<EnergyTrading.Mdm.Contracts.SourceSystem> result =
                feed.Items.Select(syndicationItem => (XmlSyndicationContent)syndicationItem.Content).Select(
                    syndic => syndic.ReadContent<EnergyTrading.Mdm.Contracts.SourceSystem>()).ToList();

            Assert.AreEqual(1, result.Where(x => x.ToMdmKey() == entity1.Id).Count(), string.Format("Entity not found in search results {0}", entity1.Id));
            Assert.AreEqual(1, result.Where(x => x.ToMdmKey() == entity2.Id).Count(), string.Format("Entity not found in search results {0}", entity2.Id));
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["SourceSystem"] + "search", content);
        }

        protected static void Establish_context()
        {
            entity1 = Script.SourceSystemData.CreateBasicEntity();
            entity2 = Script.SourceSystemData.CreateBasicEntity();

            client = new HttpClient();

			var search = new Search();
			Script.SourceSystemData.CreateSearch(search, entity1, entity2);

            content = HttpContentExtensions.CreateDataContract(search);
        }
    }
}
