namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using EnergyTrading.MDM.Extensions;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    [TestFixture]
    public class when_a_request_is_made_to_update_a_party_mapping_and_the_xml_does_not_satisfy_the_schema_ : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static HttpClient client;
        private static ulong startVersion;
        private static MDM.Party entity;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity = Script.PartyData.CreateBasicEntityWithOneMapping();
            client = new HttpClient();
            var notAMapping = new MDM.Party();
            content = HttpContentExtensions.CreateDataContract(notAMapping);
            startVersion = CurrentEntityVersion();
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString(System.Globalization.CultureInfo.InvariantCulture));

            response = client.Post(ServiceUrl["Party"] +  string.Format("{0}/Mapping/{1}", entity.Id, 
                int.MaxValue), content);
        }

        [Test]
        public void should_not_update_the_party_mapping_in_the_database()
        {
            Assert.AreEqual(startVersion, CurrentEntityVersion());
        }

        [Test]
        public void should_return_a_bad_request_status_code()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private static ulong CurrentEntityVersion()
        {
            return new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.PartyMapping>(entity.Mappings[0].Id).Version.ToUnsignedLongVersion();
        }
    }
}


