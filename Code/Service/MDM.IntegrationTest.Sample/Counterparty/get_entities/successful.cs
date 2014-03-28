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
    public class when_a_request_is_made_for_all_counterparty : IntegrationTestBase
    {
        private static IList<EnergyTrading.MDM.Contracts.Sample.Counterparty> returnedCounterpartys;

        private static MDM.Counterparty entity1;

        private static MDM.Counterparty entity2;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity1 = CounterpartyData.CreateBasicEntity();
            entity2 = CounterpartyData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Counterparty"] + "list"))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedCounterpartys = response.Content.ReadAsDataContract<CounterpartyList>();
                }
            }
        }

        [Test]
        public void should_return_the_counterparty_with_the_correct_details()
        {
            foreach (var counterparty in returnedCounterpartys)
            {
                CounterpartyDataChecker.CompareContractWithSavedEntity(counterparty);
            }
        }

        [Test]
        public void should_contain_the_new_entities_that_were_added()
        {
            IList<EnergyTrading.Mdm.Contracts.MdmId> entityIds = returnedCounterpartys.Select(x => x.Identifiers.First(id => id.IsMdmId)).ToList();
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity1.Id.ToString()));
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity2.Id.ToString()));
        }
    }
}
	
	