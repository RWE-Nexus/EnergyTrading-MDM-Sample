namespace EnergyTrading.MDM.Test
{
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    using Location = EnergyTrading.MDM.Location;

    [TestFixture]
    public class when_a_request_is_made_for_an_individual_mapping_for_a_location : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static MappingResponse mappingResponse;
        private static HttpClient client;
        private static Location entity;
        private static LocationMapping mapping;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Because_of();
        }

        protected static void Because_of()
        {
            entity = Script.LocationData.CreateBasicEntityWithOneMapping();
            mapping = entity.Mappings[0];
            client = new HttpClient(ServiceUrl["Location"] + string.Format("{0}/mapping/{1}", entity.Id, mapping.Id));

            response = client.Get();
            mappingResponse = response.Content.ReadAsDataContract<EnergyTrading.Mdm.Contracts.MappingResponse>();
        }

        [Test]
        public void should_return_the_correct_vesrion_of_the_mapping()
        {
            Assert.AreEqual(mapping.Validity.Start, mappingResponse.Mappings[0].StartDate);
            Assert.AreEqual(mapping.Validity.Finish, mappingResponse.Mappings[0].EndDate);
            Assert.AreEqual(mapping.System.Name, mappingResponse.Mappings[0].SystemName);
            Assert.IsFalse(mappingResponse.Mappings[0].IsMdmId);
            Assert.AreEqual(mapping.Id, mappingResponse.Mappings[0].MappingId);
        }

        [Test]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [Test]
        public void should_return_status_ok()
        {
            response.StatusCode = HttpStatusCode.OK;
        }

        [Test]
        public void should_return_only_one_mapping()
        {
            Assert.AreEqual(1, mappingResponse.Mappings.Count);
        }
    }
}