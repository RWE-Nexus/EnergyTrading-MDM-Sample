namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using NUnit.Framework;

    [TestFixture]
    public class when_a_request_is_made_to_create_a_sourcesystem_entity : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static EnergyTrading.Mdm.Contracts.SourceSystem sourcesystem;
        private static HttpContent content;
        private static HttpClient client;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            sourcesystem = Script.SourceSystemData.CreateContractForEntityCreation();

            content = HttpContentExtensions.CreateDataContract(sourcesystem);
            client = new HttpClient();
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["SourceSystem"], content);
            response.AssertStatusCodeIs(HttpStatusCode.Created);
        }

        [Test]
        public void should_create_an_instance_of_the_sourcesystem_in_the_database_with_the_correct_details()
        {
            Script.SourceSystemDataChecker.ConfirmEntitySaved(int.Parse(GetLocationHeader()[1]), sourcesystem);
        }

        [Test]
        public void should_return_an_created_status_code()
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public void should_return_the_location_of_the_entity()
        {
            int id;
            bool parsedInt = int.TryParse(GetLocationHeader()[1], out id);
            Assert.IsTrue(parsedInt, "The id returned was not an integer");
        }

        private string[] GetLocationHeader()
        {
            return response.Headers["Location"].Substring(0, response.Headers["Location"].IndexOf('?')).Split('/');
        }
    }
}