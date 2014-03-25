namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class BrokerDataChecker
    {
        public static void CompareContractWithEntityDetails(EnergyTrading.MDM.Contracts.Sample.Broker contract, MDM.Broker entity)
        {
            MDM.BrokerDetails correctDetail = null;

            if (entity.Details.Count == 1)
            {
                correctDetail = entity.Details[0] as MDM.BrokerDetails;
            }
            else
            {
                correctDetail = (MDM.BrokerDetails)entity.Details.Where(
                    x => x.Validity.Start >= contract.MdmSystemData.StartDate && x.Validity.Finish >= contract.MdmSystemData.EndDate).First();
            }

            Assert.AreEqual(contract.Details.Name, correctDetail.Name);
            Assert.AreEqual(contract.Details.Phone, correctDetail.Phone);
            Assert.AreEqual(contract.Details.Fax, correctDetail.Fax);
            Assert.AreEqual(contract.Details.Rate, correctDetail.Rate);
            Assert.AreEqual(contract.Party.MdmId(), entity.Party.Id);
        }

        public static void ConfirmEntitySaved(int id, EnergyTrading.MDM.Contracts.Sample.Broker contract)
        {
            var savedEntity =
                new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Broker>(id);
            contract.Identifiers.Add(new MdmId() { IsMdmId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(EnergyTrading.MDM.Contracts.Sample.Broker contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsMdmId).First().Identifier);
            var savedEntity = new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Broker>(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
