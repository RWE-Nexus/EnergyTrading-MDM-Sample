namespace EnergyTrading.MDM.Test
{
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    [TestFixture]
    public class when_a_party_is_created_and_the_latest_detail_is_updated : IntegrationTestBase
    {
        private static HttpContent content;
        private static HttpClient client;
        private static MDM.Party entity;
        private static EnergyTrading.MDM.Contracts.Sample.Party updatedContract;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = Script.PartyData.CreateBasicEntity();
            var getResponse = client.Get(ServiceUrl["Party"] + entity.Id);

            updatedContract = getResponse.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.Party>();
            updatedContract.Details.Name = "Bob";
            updatedContract.Identifiers.Remove(updatedContract.Identifiers.Where(id => id.IsMdmId).First());
            content = HttpContentExtensions.CreateDataContract(updatedContract);
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            client.Post(ServiceUrl["Party"] + entity.Id, content);
        }

        [Test]
        public void should_update_the_latest_party_details()
        {
            var party = new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Party>(entity.Id);
            Assert.AreEqual(party.LatestDetails.Name, "Bob");
        }

        [Test]
        public void should_not_create_a_new_detail()
        {
            var party = new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Party>(entity.Id);
            Assert.AreEqual(party.Details.Count, 1);
        }
    }
}

