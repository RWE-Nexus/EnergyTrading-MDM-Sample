namespace EnergyTrading.MDM.Test.ReferenceData
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    using ReferenceData = EnergyTrading.MDM.ReferenceData;

    [TestClass]
    public class when_a_request_is_made_to_delete_a_list_of_reference_data : IntegrationTestBase
    {
        private static IList<EnergyTrading.Mdm.Contracts.ReferenceData> referenceDataList;
        private static string key = "SomeRefData";
        private static HttpContent content;
        private static HttpClient client;

        private static DbSetRepository repository;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            repository = new DbSetRepository(new DbContextProvider(() => new NexusMappingContext()));

            foreach (var rd in repository.Queryable<MDM.ReferenceData>())
            {
                repository.Delete(rd);
            }

            repository.Add(new MDM.ReferenceData() { Key = key, Value = "test1" });
            repository.Add(new MDM.ReferenceData() { Key = key, Value = "test2" });
            repository.Add(new MDM.ReferenceData() { Key = key, Value = "test3" });
            repository.Flush();

            referenceDataList = new List<EnergyTrading.Mdm.Contracts.ReferenceData>() { new EnergyTrading.Mdm.Contracts.ReferenceData() { Value = "test1"}, new EnergyTrading.Mdm.Contracts.ReferenceData() { Value = "test2"}};

            content = HttpContentExtensions.CreateDataContract(referenceDataList);
            client = new HttpClient();
        }

        protected static void Because_of()
        {           
            client.Post(ServiceUrl["ReferenceData"] + string.Format("/delete/{0}", key), content);
        }

        [TestMethod]
        public void should_delete_each_of_the_reference_data_strings()
        {
            Assert.AreEqual(0, repository.Queryable<MDM.ReferenceData>().Where(x => x.Value == "test1").Count());
            Assert.AreEqual(0, repository.Queryable<MDM.ReferenceData>().Where(x => x.Value == "test2").Count());
            Assert.AreEqual(1, repository.Queryable<MDM.ReferenceData>().Where(x => x.Value == "test3").Count());
        }
    }
}

