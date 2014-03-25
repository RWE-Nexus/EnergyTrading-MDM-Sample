namespace EnergyTrading.MDM.Test
{
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;

    [TestFixture]
    public class when_a_request_is_made_to_update_a_legalentity_entity : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static EnergyTrading.MDM.Contracts.Sample.LegalEntity legalentityDataContract;
        private static HttpClient client;
        private static MDM.LegalEntity entity;

        private static EnergyTrading.MDM.Contracts.Sample.LegalEntity updatedContract;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = LegalEntityData.CreateBasicEntity();
            var getResponse = client.Get(ServiceUrl["LegalEntity"] + entity.Id);

            updatedContract = getResponse.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.LegalEntity>();
            content = HttpContentExtensions.CreateDataContract(LegalEntityData.MakeChangeToContract(updatedContract));
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            response = client.Post(ServiceUrl["LegalEntity"] + entity.Id, content);
            response.AssertStatusCodeIs(HttpStatusCode.NoContent);
        }

        [Test]
        public void should_update_the_legalentity_in_the_database_with_the_correct_details()
        {
            LegalEntityDataChecker.ConfirmEntitySaved(entity.Id, updatedContract);
        }

        [Test]
        public void should_return_a_no_content_status_code()
        {
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
