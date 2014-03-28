namespace EnergyTrading.MDM.Test
{
    using System.Configuration;
    using System.Linq;
    using System.Net;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    [TestFixture]
    public class when_a_request_is_made_to_delete_a_broker_mapping : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpClient client;
        private static MDM.Broker broker;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            broker = BrokerData.CreateEntityWithTwoDetailsAndTwoMappings();
        }

        protected static void Because_of()
        {
            client = new HttpClient();
            var uri = ServiceUrl["Broker"] + broker.Id + "/Mapping/" + broker.Mappings[0].Id;
            response = client.Delete(uri);
        }

        [Test]
        public void should_delete_the_mapping()
        {
            var dbBroker =
                new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Broker>(broker.Id);

            Assert.IsTrue(dbBroker.Mappings.Where(mapping => mapping.Id == broker.Mappings[0].Id).Count() == 0);
        }

        [Test]
        public void should_leave_other_mappings_untouched()
        {
            var dbBroker =
                new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Broker>(broker.Id);

            Assert.AreEqual(1, dbBroker.Mappings.Count);
        }

        [Test]
        public void should_return_status_ok()
        {
            response.StatusCode = HttpStatusCode.OK;
        }
    }

}
    