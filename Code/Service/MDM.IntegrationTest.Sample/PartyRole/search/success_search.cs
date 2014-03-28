namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Search;

    [TestClass]
    public class when_a_search_for_a_partyrole_is_made_with_a_specific_name : IntegrationTestBase
    {
        private static HttpClient client;

        private static HttpContent content;

        private static HttpResponseMessage response;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        [TestMethod]
        public void should_return_a_status_code_of_Ok()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void should_return_the_content_of_the_search_results()
        {
            Assert.IsTrue(response.Headers["Content-Type"].ToLowerInvariant().StartsWith("application/xml"));
        }

        protected static void Because_of()
        {
            client.TransportSettings.MaximumAutomaticRedirections = 0;
            response = client.Post(ServiceUrl["PartyRole"] + "Search", content);
        }

        protected static void Establish_context()
        {
            var entity1 = PartyRoleData.CreateBasicEntity();
            var entity2 = PartyRoleData.CreateBasicEntity();

            client = new HttpClient();

			var search = new Search();
			PartyRoleData.CreateSearch(search, entity1, entity2);

            content = HttpContentExtensions.CreateDataContract(search);
        }

        private string[] GetLocationHeader()
        {
            return response.Headers["Location"].Split('/');
        }
    }
}
