namespace RWEST.Nexus.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;
    using RWEST.Nexus.Data.EntityFramework;
    using RWEST.Nexus.MDM.Data.EF.Configuration;

    [TestClass]
    public class when_a_request_is_made_to_update_a_curve_entity_and_the_xml_does_not_satisfy_the_schema_ : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static HttpClient client;
        private static MDM.Curve entity;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            var notACurve = new Mapping();
            content = HttpContentExtensions.CreateDataContract(notACurve);
        }

        protected static void Because_of()
        {
            entity = CurveData.CreateBasicEntity();
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            response = client.Post(ServiceUrl["Curve"] + entity.Id, content);
        }

        [TestMethod]
        public void should_not_update_the_curve_in_the_database()
        {
            Assert.AreEqual(entity.Version, CurrentEntityVersion());
        }

        [TestMethod]
        public void should_return_a_bad_request_status_code()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private static long CurrentEntityVersion()
        {
            return new DbSetRepository<MDM.Curve>(new MappingContext()).FindOne<MDM.Curve>(entity.Id).Version;
        }
    }
}

