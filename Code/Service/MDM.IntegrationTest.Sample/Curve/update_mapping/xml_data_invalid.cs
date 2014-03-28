namespace RWEST.Nexus.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.Data.EntityFramework;
    using RWEST.Nexus.MDM.Data.EF.Configuration;

    [TestClass]
    public class when_a_request_is_made_to_update_a_curve_mapping_and_the_xml_does_not_satisfy_the_schema_ : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static HttpClient client;
        private static long startVersion;
        private static MDM.Curve entity;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity = CurveData.CreateBasicEntityWithOneMapping();
            client = new HttpClient();
            var notAMapping = new MDM.Curve();
            content = HttpContentExtensions.CreateDataContract(notAMapping);
            startVersion = CurrentEntityVersion();
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", BitConverter.ToInt64(entity.Mappings[0].Version, 0).ToString());

            response = client.Post(ServiceUrl["Curve"] +  string.Format("{0}/Mapping/{1}", entity.Id, 
                int.MaxValue), content);
        }

        [TestMethod]
        public void should_not_update_the_curve_mapping_in_the_database()
        {
            Assert.AreEqual(startVersion, CurrentEntityVersion());
        }

        [TestMethod]
        public void should_return_a_bad_request_status_code()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private static long CurrentEntityVersion()
        {
            byte[] b = new DbSetRepository<MDM.CurveMapping>(new MappingContext()).FindOne<MDM.Curve>(entity.Mappings[0].Id).Version;
            return BitConverter.ToInt64(b, 0);
        }
    }
}


