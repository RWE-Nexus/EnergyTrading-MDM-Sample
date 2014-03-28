namespace EnergyTrading.MDM.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Linq;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    [TestFixture]
    public class when_a_request_is_made_for_all_broker : IntegrationTestBase
    {
        private static IList<EnergyTrading.MDM.Contracts.Sample.Broker> returnedBrokers;

        private static MDM.Broker entity1;

        private static MDM.Broker entity2;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity1 = BrokerData.CreateBasicEntity();
            entity2 = BrokerData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Broker"] + "list"))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedBrokers = response.Content.ReadAsDataContract<BrokerList>();
                }
            }
        }

        [Test]
        public void should_return_the_broker_with_the_correct_details()
        {
            foreach (var broker in returnedBrokers)
            {
                BrokerDataChecker.CompareContractWithSavedEntity(broker);
            }
        }

        [Test]
        public void should_contain_the_new_entities_that_were_added()
        {
            IList<EnergyTrading.Mdm.Contracts.MdmId> entityIds = returnedBrokers.Select(x => x.Identifiers.First(id => id.IsMdmId)).ToList();
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity1.Id.ToString()));
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity2.Id.ToString()));
        }
    }
}
	
	