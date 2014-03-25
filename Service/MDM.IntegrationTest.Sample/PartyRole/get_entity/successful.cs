namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_partyrole_and_they_exist : IntegrationTestBase
    {
        private static MDM.PartyRole partyrole;

        private static OpenNexus.MDM.Contracts.PartyRole returnedPartyRole;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            partyrole = PartyRoleData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["PartyRole"] + 
                partyrole.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedPartyRole = response.Content.ReadAsDataContract<OpenNexus.MDM.Contracts.PartyRole>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_partyrole_with_the_correct_details()
        {
            PartyRoleDataChecker.CompareContractWithSavedEntity(returnedPartyRole);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_partyrole_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.PartyRole partyrole;
        private static OpenNexus.MDM.Contracts.PartyRole returnedPartyRole;
        private static DateTime asof;
        private static HttpClient client;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            partyrole = PartyRoleData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["PartyRole"] + string.Format("{0}?as-of={1}",
                    partyrole.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedPartyRole = response.Content.ReadAsDataContract<OpenNexus.MDM.Contracts.PartyRole>();
        }

        [TestMethod]
        public void should_return_the_partyrole_with_the_correct_details()
        {
            PartyRoleDataChecker.CompareContractWithSavedEntity(returnedPartyRole);
        }
    }
}