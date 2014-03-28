namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using EnergyTrading;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;


    [TestClass]
    public class when_a_request_is_made_to_create_a_partyrole_mapping : IntegrationTestBase
    {
        private static HttpResponseMessage response;

        private static EnergyTrading.Mdm.Contracts.Mapping mapping;

        private static HttpContent content;

        private static HttpClient client;

        private static MDM.PartyRole entity;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity = PartyRoleData.CreateBasicEntity();
            mapping = new EnergyTrading.Mdm.Contracts.Mapping{
                    SystemName = "Endur",
                    Identifier = Guid.NewGuid().ToString(),
                    SourceSystemOriginated = false,
                    DefaultReverseInd = false,
                    StartDate = Script.baseDate,
                    EndDate = Script.baseDate.AddDays(2)
                };

            content = HttpContentExtensions.CreateDataContract(mapping);
            client = new HttpClient();
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["PartyRole"] + string.Format("{0}/Mapping", entity.Id), content);
        }

        [TestMethod]
        public void should_create_a_mapping_on_the_partyrole_entity()
        {
            var savedMapping =
                new DbSetRepository(new DbContextProvider(() => new NexusMappingContext())).FindOne<MDM.PartyRole>(int.Parse(GetLocationHeader()[3]));

            Assert.AreEqual(mapping.SystemName, savedMapping.System.Name);
            Assert.AreEqual(mapping.Identifier, savedMapping.MappingValue);
            Assert.AreEqual(mapping.SourceSystemOriginated, savedMapping.IsMaster);
            Assert.AreEqual(mapping.DefaultReverseInd, savedMapping.IsDefault);
            Assert.AreEqual(DateUtility.Round(mapping.StartDate.Value), savedMapping.Validity.Start);
            Assert.AreEqual(DateUtility.Round(mapping.EndDate.Value), savedMapping.Validity.Finish);
        }

        [TestMethod]
        public void should_return_an_created_status_code()
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void should_return_the_location_of_the_entity()
        {
            int id;
            bool parsedEntityId = int.TryParse(GetLocationHeader()[1], out id);
            bool parsedMappingId = int.TryParse(GetLocationHeader()[3], out id);
            Assert.IsTrue(parsedEntityId, "The PartyRole id returned was not an integer");
            Assert.IsTrue(parsedMappingId, "The mapping id returned was not an integer");
            Assert.AreEqual("Mapping", GetLocationHeader()[2], true);
        }

        private string[] GetLocationHeader()
        {
            return response.Headers["Location"].Split('/');
        }
    }
}
	