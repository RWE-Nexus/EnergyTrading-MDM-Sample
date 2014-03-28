namespace EnergyTrading.MDM.Test.bug_fix
{
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    [TestFixture]
    [Ignore]
    public class when_a_request_is_made_to_create_a_new_location_and_there_is_already_a_location_with_the_same_name : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static EnergyTrading.MDM.Contracts.Sample.Location location;
        private static HttpContent content;
        private static HttpClient client;

        private static HttpContent sameContent;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            location = Script.LocationData.CreateContractForEntityCreation();

            content = HttpContentExtensions.CreateDataContract(location);
            sameContent = HttpContentExtensions.CreateDataContract(location);
            client = new HttpClient();
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["Location"], content);
            response = client.Post(ServiceUrl["Location"], sameContent);
        }

        [Test]
        public void should_return_a_fault_that_contains_a_message_informing_of_the_unique_key_constraint()
        {
            var fault = response.Content.ReadAsDataContract<Fault>();
            Assert.IsTrue(fault.Message.Contains("Violation of UNIQUE KEY constraint 'CK_Location'"), 
                "The message should show the actual inner exception thrown by EF.");
        }
    }
}