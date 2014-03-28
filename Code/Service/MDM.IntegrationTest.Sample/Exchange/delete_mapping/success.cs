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
    public class when_a_request_is_made_to_delete_a_exchange_mapping : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpClient client;
        private static MDM.Exchange exchange;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            exchange = ExchangeData.CreateEntityWithTwoDetailsAndTwoMappings();
        }

        protected static void Because_of()
        {
            client = new HttpClient();
            var uri = ServiceUrl["Exchange"] + exchange.Id + "/Mapping/" + exchange.Mappings[0].Id;
            response = client.Delete(uri);
        }

        [Test]
        public void should_delete_the_mapping()
        {
            var dbExchange =
                new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Exchange>(exchange.Id);

            Assert.IsTrue(dbExchange.Mappings.Where(mapping => mapping.Id == exchange.Mappings[0].Id).Count() == 0);
        }

        [Test]
        public void should_leave_other_mappings_untouched()
        {
            var dbExchange =
                new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Exchange>(exchange.Id);

            Assert.AreEqual(1, dbExchange.Mappings.Count);
        }

        [Test]
        public void should_return_status_ok()
        {
            response.StatusCode = HttpStatusCode.OK;
        }
    }

}
    