namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;

    [TestFixture]
    public class when_a_request_is_made_for_a_legalentity_and_they_exist : IntegrationTestBase
    {
        private static MDM.LegalEntity legalentity;

        private static EnergyTrading.MDM.Contracts.Sample.LegalEntity returnedLegalEntity;

        [TestFixtureSetUp]
        public static void ClassInit()
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            legalentity = LegalEntityData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["LegalEntity"] + 
                legalentity.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedLegalEntity = response.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.LegalEntity>();
                }
            }
        }

        [Test]
        public void should_return_the_legalentity_with_the_correct_details()
        {
            LegalEntityDataChecker.CompareContractWithSavedEntity(returnedLegalEntity);
        }
    }

    [TestFixture]
    public class when_a_request_is_made_for_a_legalentity_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.LegalEntity legalentity;
        private static EnergyTrading.MDM.Contracts.Sample.LegalEntity returnedLegalEntity;
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
            legalentity = LegalEntityData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["LegalEntity"] + string.Format("{0}?as-of={1}",
                    legalentity.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedLegalEntity = response.Content.ReadAsDataContract<EnergyTrading.MDM.Contracts.Sample.LegalEntity>();
        }

        [Test]
        public void should_return_the_legalentity_with_the_correct_details()
        {
            LegalEntityDataChecker.CompareContractWithSavedEntity(returnedLegalEntity);
        }
    }
}