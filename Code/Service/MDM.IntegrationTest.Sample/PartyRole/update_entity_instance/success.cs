namespace EnergyTrading.MDM.Test
{
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_to_update_a_partyrole_entity : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static OpenNexus.MDM.Contracts.PartyRole partyDataContract;
        private static HttpClient client;
        private static MDM.PartyRole entity;

        private static OpenNexus.MDM.Contracts.PartyRole updatedContract;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = PartyRoleData.CreateBasicEntity();
            var getResponse = client.Get(ServiceUrl["PartyRole"] + entity.Id);

            updatedContract = getResponse.Content.ReadAsDataContract<OpenNexus.MDM.Contracts.PartyRole>();
            content = HttpContentExtensions.CreateDataContract(PartyRoleData.MakeChangeToContract(updatedContract));
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            response = client.Post(ServiceUrl["PartyRole"] + entity.Id, content);
        }

        [TestMethod]
        public void should_update_the_partyrole_in_the_database_with_the_correct_details()
        {
            PartyRoleDataChecker.ConfirmEntitySaved(entity.Id, updatedContract);
        }

        [TestMethod]
        public void should_return_a_no_content_status_code()
        {
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}

