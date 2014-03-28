namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    [TestFixture, Ignore]
    public class when_a_request_is_made_to_create_a_location_mapping_and_a_conflicting_mapping_exists : IntegrationTestBase
    {
        private static HttpResponseMessage response2;

        private static EnergyTrading.Mdm.Contracts.Mapping mapping;

        private static HttpContent content;

        private static HttpContent content2;

        private static HttpClient client;

        private static MDM.Location entity;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity = Script.LocationData.CreateBasicEntity();
            mapping = new EnergyTrading.Mdm.Contracts.Mapping{
                    SystemName = "Endur",
                    Identifier = "Test",
                    SourceSystemOriginated = false,
                    DefaultReverseInd = true,
                    StartDate = Script.baseDate,
                    EndDate = Script.baseDate.AddDays(2)
                };

            content = HttpContentExtensions.CreateDataContract(mapping);
            content2 = HttpContentExtensions.CreateDataContract(mapping);
            client = new HttpClient();
        }

        protected static void Because_of()
        {
            client.Post(ServiceUrl["Location"] + string.Format("{0}/Mapping", entity.Id), content);
            response2 = client.Post(ServiceUrl["Location"] + string.Format("{0}/Mapping", entity.Id), content2);
        }

        [Test]
        public void should_return_an_created_status_code()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, response2.StatusCode);
        }
    }
}

