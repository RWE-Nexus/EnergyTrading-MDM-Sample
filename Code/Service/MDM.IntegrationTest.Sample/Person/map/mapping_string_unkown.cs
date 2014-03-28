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
    public class when_a_request_is_made_to_retrive_a_person_from_a_source_system_and_the_mapping_string_doesnt_exist : IntegrationTestBase
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
            client = new HttpClient(ServiceUrl["Person"] +
            "map?source-system=trayport&mapping-string=xxx&as-of=2010-03-16T11:21:23Z");
            response = client.Get();
        }

        [Test]
        public void should_not_return_a_person()
        {
            EnergyTrading.MDM.Contracts.Sample.Person returnedPerson = null;
            try { returnedPerson = response.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.Person>(); } catch { }
            Assert.IsNull(returnedPerson);
        }

        [Test]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [Test]
        public void should_return_not_found_status_code()
        {
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        [Ignore]
        // This test relates to a feature to return the correct message dependant on if a map fails because a source
        // system is missing or a mapping is missing
        // http://195.157.55.156/VersionOne/assetdetail.v1?oid=Task%3a18716
        public void should_return_nexus_failure_with_correct_information()
        {
            Fault fault = null;
            try { fault = response.Content.ReadAsDataContract<Fault>(); } catch { }

            Assert.IsNotNull(fault);
            Assert.AreEqual("Unknown Mapping", fault.Reason);
            Assert.AreEqual( new DateTime(2010, 03, 16, 11, 21, 23), fault.AsOfDate.Value);
            Assert.That("Trayport", Is.EqualTo(fault.SourceSystem).IgnoreCase);
            Assert.That("Person Mapping String 'abc' not found for Source System 'Trayport' and the given date '2010-03-16T11:21:23Z'", Is.EqualTo(fault.Message).IgnoreCase);
        }
    }
}