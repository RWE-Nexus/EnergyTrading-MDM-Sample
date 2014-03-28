namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;

    using Person = EnergyTrading.MDM.Person;

    [TestFixture]
    public class when_a_person_entity_is_not_valid_and_the_list_function_is_called : IntegrationTestBase
    {
        private static HttpContent content;
        private static HttpClient client;
        private static MDM.Person entity;
        private static EnergyTrading.MDM.Contracts.Sample.Person updatedContract;
        private static Person entity2;
        private static Person entity3;

        private static PersonList returnedPersons;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = Script.PersonData.CreateBasicEntity();
            entity2 = Script.PersonData.CreateBasicEntity();
            entity3 = Script.PersonData.CreateBasicEntity();
            var getResponse = client.Get(ServiceUrl["Person"] + entity.Id);

            updatedContract = getResponse.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.Person>();
            updatedContract.MdmSystemData.EndDate = DateTime.Now.Subtract(new TimeSpan(1,0,0,0,0));

            updatedContract.Identifiers.Remove(updatedContract.Identifiers.Where(id => id.IsMdmId).First());
            content = HttpContentExtensions.CreateDataContract(updatedContract);
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            client.Post(ServiceUrl["Person"] + entity.Id, content);
        }

        protected static void Because_of()
        {
            using (var client2 = new HttpClient(ServiceUrl["Person"] + "list"))
            {
                using (HttpResponseMessage response = client2.Get())
                {
                    returnedPersons = response.Content.ReadAsDataContract<PersonList>();
                }
            }
        }

        [Test]
        public void should_not_return_the_invalid_person()
        {
            var person = new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Person>(entity.Id);
            Assert.AreEqual(0, returnedPersons.Where(person1 => person1.NexusId() == entity.Id).Count());
        }
    }
}


