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
    public class when_a_request_is_made_for_all_location : IntegrationTestBase
    {
        private static IList<EnergyTrading.MDM.Contracts.Sample.Location> returnedLocations;

        private static MDM.Location entity1;

        private static MDM.Location entity2;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity1 = Script.LocationData.CreateBasicEntity();
            entity2 = Script.LocationData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Location"] + "list"))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedLocations = response.Content.ReadAsDataContract<LocationList>();
                }
            }
        }

        [Test]
        //TODO: RobH Timing issue needs to be repaired
        public void should_return_the_location_with_the_correct_details()
        {
            foreach (var location in returnedLocations)
            {
                Script.LocationDataChecker.CompareContractWithSavedEntity(location);
            }
        }

        [Test]
        public void should_contain_the_new_entities_that_were_added()
        {
            IList<EnergyTrading.Mdm.Contracts.MdmId> entityIds = returnedLocations.Select(x => x.Identifiers.First(id => id.IsMdmId)).ToList();
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity1.Id.ToString()));
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity2.Id.ToString()));
        }
    }
}
	
	