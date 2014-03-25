namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Data.SqlTypes;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    [TestFixture]
    public class when_a_source_system_to_master_data_service_mapping_request_is_made_as_of_a_specific_date_party  : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static PartyDetails firstDetails;
        private static HttpClient client;
        private static MDM.Party party;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            party = Script.PartyData.CreateEntityWithTwoDetailsAndTwoMappings();
        }

        protected static void Because_of()
        {
            client = new HttpClient(ServiceUrl["Party"] +
                    "map?source-system=Trayport&mapping-string=" + party.Mappings[0].MappingValue + "&as-of=" +
                    party.Validity.Start.ToString(DateFormatString));

            response = client.Get();
        }

        [Test]
        public void should_return_the_correct_vesrion_of_the_party()
        {
            var party = response.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.Party>();

            Script.PartyDataChecker.CompareContractWithSavedEntity(party);
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
    }

    [TestFixture]
    public class when_a_source_system_to_master_data_service_mapping_request_is_made_as_of_now_for_party : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpClient client;
        private static MDM.Party party;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            party = Script.PartyData.CreateEntityWithTwoDetailsAndTwoMappings();
        }

        protected static void Because_of()
        {
            client = new HttpClient(ServiceUrl["Party"] +
                "map?source-system=Trayport&mapping-string=" + party.Mappings[0].MappingValue);

            response = client.Get();
        }

        [Test]
        public void should_return_the_correct_vesrion_of_the_party()
        {
            var party = response.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.Party>();

            Script.PartyDataChecker.CompareContractWithSavedEntity(party);
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
    }
}