namespace EnergyTrading.MDM.Test
{
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;

    [TestFixture]
    public class when_a_request_is_made_to_update_a_party_entity : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static EnergyTrading.MDM.Contracts.Sample.Party partyDataContract;
        private static HttpClient client;
        private static MDM.Party entity;

        private static EnergyTrading.MDM.Contracts.Sample.Party updatedContract;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = Script.PartyData.CreateBasicEntity();
            var getResponse = client.Get(ServiceUrl["Party"] + entity.Id);

            updatedContract = getResponse.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.Party>();
            content = HttpContentExtensions.CreateDataContract(Script.PartyData.MakeChangeToContract(updatedContract));
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            response = client.Post(ServiceUrl["Party"] + entity.Id, content);
        }

        [Test]
        public void should_update_the_party_in_the_database_with_the_correct_details()
        {
            Script.PartyDataChecker.ConfirmEntitySaved(entity.Id, updatedContract);
        }

        [Test]
        public void should_return_a_no_content_status_code()
        {
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
