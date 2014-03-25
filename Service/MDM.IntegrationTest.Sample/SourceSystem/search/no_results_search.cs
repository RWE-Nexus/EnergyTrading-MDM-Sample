namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Search;

    [TestFixture]
    public class when_a_search_for_a_sourcesystem_is_made_with_a_specific_name_and_no_results_are_found : IntegrationTestBase
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
        public void should_return_a_status_code_of_not_found()
        {
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public void should_not_return_a_location_for_the_search()
        {
            Assert.IsFalse(response.Headers.ContainsKey("Location"), "Location shouldn't be added to the header values");
        }

        protected static void Because_of()
        {
            client.TransportSettings.MaximumAutomaticRedirections = 0;
            response = client.Post(ServiceUrl["SourceSystem"] + "Search", content);
        }

        protected static void Establish_context()
        {
            client = new HttpClient();

            Search search = SearchBuilder.CreateSearch();
            search.AddSearchCriteria(SearchCombinator.Or).AddCriteria(
                "x", SearchCondition.Equals, Guid.NewGuid().ToString());

            content = HttpContentExtensions.CreateDataContract(search);
        }
    }
}

