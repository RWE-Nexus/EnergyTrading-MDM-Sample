namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Search;

    [TestFixture]
    public class when_a_search_for_a_broker_is_made_with_a_specific_name : IntegrationTestBase
    {
        private static HttpClient client;

        private static HttpContent content;

        private static HttpResponseMessage response;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        [Test]
        public void should_return_a_status_code_of_Ok()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        protected static void Because_of()
        {
            client.TransportSettings.MaximumAutomaticRedirections = 0;
            response = client.Post(ServiceUrl["Broker"] + "Search", content);
        }

        protected static void Establish_context()
        {
            var entity1 = BrokerData.CreateBasicEntity();
            var entity2 = BrokerData.CreateBasicEntity();

            client = new HttpClient();

			var search = new Search();
			BrokerData.CreateSearch(search, entity1, entity2);

            content = HttpContentExtensions.CreateDataContract(search);
        }
    }
}
