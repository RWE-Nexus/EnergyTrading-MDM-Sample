namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Search;

    [TestFixture]
    [Ignore]
    public class when_a_search_for_a_party_is_made_with_a_specific_name : IntegrationTestBase
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
        [Ignore()]
        public void should_return_a_status_code_of_Ok()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [Ignore()]
        public void should_return_the_content_of_the_search_results()
        {
            Assert.IsTrue(response.Headers["Content-Type"].ToLowerInvariant().StartsWith("application/xml"));
        }

        protected static void Because_of()
        {
            client.TransportSettings.MaximumAutomaticRedirections = 0;
            response = client.Post(ServiceUrl["Party"] + "Search", content);
        }

        protected static void Establish_context()
        {
            var entity1 = Script.PartyData.CreateBasicEntity();
            var entity2 = Script.PartyData.CreateBasicEntity();

            client = new HttpClient();

			var search = new Search();
			Script.PartyData.CreateSearch(search, entity1, entity2);

            content = HttpContentExtensions.CreateDataContract(search);
        }

        private string[] GetLocationHeader()
        {
            return response.Headers["Location"].Split('/');
        }
    }
}
