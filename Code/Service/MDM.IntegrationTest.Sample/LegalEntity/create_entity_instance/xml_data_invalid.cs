﻿namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    [TestFixture]
    public class when_a_request_is_made_to_create_a_legalentity_entity_and_the_xml_does_not_satisfy_the_schema : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static HttpClient client;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            var notAPerson = new Mapping();
            content = HttpContentExtensions.CreateDataContract(notAPerson);
            client = new HttpClient();
        }

        protected static void Because_of()
        {
            response = client.Post(new Uri(ServiceUrl["Person"]), content);
        }

        [Test]
        public void should_return_bad_request_status_code()
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }
    }
}