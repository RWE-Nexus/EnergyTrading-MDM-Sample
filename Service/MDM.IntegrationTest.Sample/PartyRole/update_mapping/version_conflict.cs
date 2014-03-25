namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    [TestClass]
    public class when_a_request_is_made_to_update_a_partyrole_mapping_and_the_etag_is_not_current : IntegrationTestBase
    {
        private static HttpResponseMessage response;

        private static EnergyTrading.Mdm.Contracts.Mapping mapping;

        private static HttpContent content;

        private static HttpClient client;

        private static PartyRoleMapping currentTrayportMapping;

        private static ulong startVersion;

        private static MDM.PartyRole entity;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity = PartyRoleData.CreateBasicEntityWithOneMapping();
            currentTrayportMapping = entity.Mappings[0];

            mapping = new EnergyTrading.Mdm.Contracts.Mapping{
                
                    SystemName = currentTrayportMapping.System.Name,
                    Identifier = currentTrayportMapping.MappingValue,
                    SourceSystemOriginated = currentTrayportMapping.IsMaster,
                    DefaultReverseInd = currentTrayportMapping.IsDefault,
                    StartDate = currentTrayportMapping.Validity.Start,
                    EndDate = currentTrayportMapping.Validity.Finish.AddDays(2)
                };

            content = HttpContentExtensions.CreateDataContract(mapping);
            client = new HttpClient();
            startVersion = CurrentEntityVersion();
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", long.MaxValue.ToString());

            response = client.Post(ServiceUrl["PartyRole"] +  string.Format("{0}/Mapping/{1}", entity.Id, 
                entity.Mappings[0].Id), content);
        }

        [TestMethod]
        public void should_not_update_the_partyrole_in_the_database()
        {
            Assert.AreEqual(startVersion, CurrentEntityVersion());
        }

        [TestMethod]
        public void should_return_a_no_content_status_code()
        {
            Assert.AreEqual(HttpStatusCode.PreconditionFailed, response.StatusCode);
        }

        private static ulong CurrentEntityVersion()
        {
            return new DbSetRepository(new DbContextProvider(() => new NexusMappingContext())).FindOne<MDM.PartyRole>(entity.Id).Version;
        }
    }
}
