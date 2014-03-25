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
    public class when_a_source_system_to_master_data_service_mapping_request_is_made_as_of_a_specific_date_exchange  : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static ExchangeDetails firstDetails;
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
            client = new HttpClient(ServiceUrl["Exchange"] +
                    "map?source-system=Trayport&mapping-string=" + exchange.Mappings[0].MappingValue + "&as-of=" +
                    exchange.Validity.Start.ToString(DateFormatString));

            response = client.Get();
        }

        [Test]
        public void should_return_the_correct_vesrion_of_the_exchange()
        {
            var exchange = response.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.Exchange>();

            ExchangeDataChecker.CompareContractWithSavedEntity(exchange);
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
    public class when_a_source_system_to_master_data_service_mapping_request_is_made_as_of_now_for_exchange : IntegrationTestBase
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
            client = new HttpClient(ServiceUrl["Exchange"] +
                "map?source-system=Trayport&mapping-string=" + exchange.Mappings[0].MappingValue);

            response = client.Get();
        }

        [Test]
        public void should_return_the_correct_vesrion_of_the_exchange()
        {
            var exchange = response.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.Exchange>();

            ExchangeDataChecker.CompareContractWithSavedEntity(exchange);
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