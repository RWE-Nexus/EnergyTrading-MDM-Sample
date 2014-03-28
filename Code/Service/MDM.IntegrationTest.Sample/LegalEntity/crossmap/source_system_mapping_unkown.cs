namespace EnergyTrading.MDM.Test
{
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    [TestFixture]
    public class when_a_source_system_to_destination_system_mapping_request_is_made_and_the_source_system_mapping_string_is_unkown_for_legalentity : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpClient client;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Because_of();
        }

        protected static void Because_of()
        {
            client = new HttpClient(ServiceUrl["LegalEntity"] +
                "crossmap?source-system=trayport&destination-system=endur&mapping-string=abc");

            response = client.Get();
        }

        [Test]
        public void should_return_nexus_failure_with_correct_information()
        {
            Fault fault = null;
            try { fault = response.Content.ReadAsDataContract<Fault>(); } catch { }

            Assert.IsNotNull(fault);
            Assert.AreEqual("Unknown Mapping", fault.Reason);
            Assert.AreEqual("abc", fault.Mapping);
            Assert.That("Trayport", Is.EqualTo(fault.SourceSystem).IgnoreCase);
            Assert.IsNull(fault.AsOfDate);
            Assert.That(
                "Mapping String 'abc' not found for Source System 'Trayport'",
                Is.EqualTo(fault.Message).IgnoreCase);
        }

        [Test]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [Test]
        public void should_return_status_not_found()
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }
    }
}