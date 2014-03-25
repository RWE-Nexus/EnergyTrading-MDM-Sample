namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Configuration;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    [TestFixture]
    public class when_a_request_is_made_to_retrive_a_location_from_a_source_system_and_the_source_system_doesnt_exist : IntegrationTestBase
    {
        private static HttpClient client;
        private static HttpResponseMessage response;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Because_of();
        }

        protected static void Because_of()
        {
            client = new HttpClient(ServiceUrl["Location"] +
            "map?source-system=missing_system&mapping-string=xxx&as-of=2010-03-16T11:21:23Z");
            response = client.Get();
        }

        [Test]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [Test]
        public void should_return_not_found_status_code()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public void should_return_validation_error()
        {
            Fault fault = null;
            try
            {
                fault = response.Content.ReadAsDataContract<Fault>();
            }
            catch
            {
            }

            Assert.IsNotNull(fault);
            Assert.AreEqual("Validation failure", fault.Reason);
            Assert.IsTrue(fault.Message.Contains("No system named 'missing_system' was found"));
        }
    }
}