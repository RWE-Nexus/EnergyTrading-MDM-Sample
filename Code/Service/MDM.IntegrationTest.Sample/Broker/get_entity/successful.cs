namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;

    [TestFixture]
    public class when_a_request_is_made_for_a_broker_and_they_exist : IntegrationTestBase
    {
        private static MDM.Broker broker;

        private static EnergyTrading.MDM.Contracts.Sample.Broker returnedBroker;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            broker = BrokerData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Broker"] + 
                broker.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedBroker = response.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.Broker>();
                }
            }
        }

        [Test]
        public void should_return_the_broker_with_the_correct_details()
        {
            BrokerDataChecker.CompareContractWithSavedEntity(returnedBroker);
        }
    }

    [TestFixture]
    public class when_a_request_is_made_for_a_broker_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Broker broker;
        private static EnergyTrading.MDM.Contracts.Sample.Broker returnedBroker;
        private static DateTime asof;
        private static HttpClient client;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            broker = BrokerData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Broker"] + string.Format("{0}?as-of={1}",
                    broker.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedBroker = response.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.Broker>();
        }

        [Test]
        public void should_return_the_broker_with_the_correct_details()
        {
            BrokerDataChecker.CompareContractWithSavedEntity(returnedBroker);
        }
    }
}