namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using EnergyTrading;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;


    [TestFixture]
    public class when_a_request_is_made_to_create_a_location_mapping : IntegrationTestBase
    {
        private static HttpResponseMessage response;

        private static EnergyTrading.Mdm.Contracts.Mapping mapping;

        private static HttpContent content;

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
            response = client.Post(ServiceUrl["Location"] + string.Format("{0}/Mapping", entity.Id), content);
        }

        [Test]
        public void should_create_a_mapping_on_the_location_entity()
        {
            var savedMapping =
                new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.LocationMapping>(int.Parse(GetLocationHeader()[3]));

            Assert.AreEqual(mapping.SystemName, savedMapping.System.Name);
            Assert.AreEqual(mapping.Identifier, savedMapping.MappingValue);
            Assert.AreEqual(mapping.SourceSystemOriginated, savedMapping.IsMaster);
            Assert.AreEqual(mapping.DefaultReverseInd, savedMapping.IsDefault);
            Assert.AreEqual(DateUtility.Round(mapping.StartDate.Value), savedMapping.Validity.Start);
            Assert.AreEqual(DateUtility.Round(mapping.EndDate.Value), savedMapping.Validity.Finish);
        }

        [Test]
        public void should_return_an_created_status_code()
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public void should_return_the_location_of_the_entity()
        {
            //Assert.AreEqual("Location", GetLocationHeader()[0], true);
            int id;
            bool parsedEntityId = int.TryParse(GetLocationHeader()[1], out id);
            bool parsedMappingId = int.TryParse(GetLocationHeader()[3], out id);
            Assert.IsTrue(parsedEntityId, "The Location id returned was not an integer");
            Assert.IsTrue(parsedMappingId, "The mapping id returned was not an integer");
            Assert.That("Mapping", Is.EqualTo(GetLocationHeader()[2]).IgnoreCase);
        }

        private string[] GetLocationHeader()
        {
            return response.Headers["Location"].Split('/');
        }
    }
}
	