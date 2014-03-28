namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_to_update_a_partyrole_entity_and_the_entity_is_not_found_ : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static HttpClient client;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            var partyrole = new OpenNexus.MDM.Contracts.PartyRole();
            content = HttpContentExtensions.CreateDataContract(partyrole);
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", 0.ToString());
            response = client.Post(ServiceUrl["PartyRole"] + int.MaxValue, content);
        }

        [TestMethod]
        public void should_return_a_not_found_status_code()
        {
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
