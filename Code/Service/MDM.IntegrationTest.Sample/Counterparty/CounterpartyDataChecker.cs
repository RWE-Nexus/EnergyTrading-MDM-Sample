namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class CounterpartyDataChecker
    {
        public static void CompareContractWithEntityDetails(EnergyTrading.MDM.Contracts.Sample.Counterparty contract, MDM.Counterparty entity)
        {
            MDM.CounterpartyDetails correctDetail = null;

            if (entity.Details.Count == 1)
            {
                correctDetail = entity.Details[0] as MDM.CounterpartyDetails;
            }
            else
            {
                correctDetail = (MDM.CounterpartyDetails)entity.Details.Where(
                    x => x.Validity.Start >= contract.MdmSystemData.StartDate && x.Validity.Finish >= contract.MdmSystemData.EndDate).First();
            }

            Assert.AreEqual(contract.Details.Name, correctDetail.Name);
            Assert.AreEqual(contract.Details.Phone, correctDetail.Phone);
            Assert.AreEqual(contract.Details.Fax, correctDetail.Fax);
            Assert.AreEqual(contract.Details.ShortName, correctDetail.ShortName);
            //Assert.AreEqual(contract.Details.TaxLocation.MdmId().Value, correctDetail.TaxLocation.Id);
            Assert.AreEqual(contract.Party.MdmId().Value, entity.Party.Id);
        }

        public static void ConfirmEntitySaved(int id, EnergyTrading.MDM.Contracts.Sample.Counterparty contract)
        {
            var savedEntity =
                new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Counterparty>(id);
            contract.Identifiers.Add(new MdmId() { IsMdmId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(EnergyTrading.MDM.Contracts.Sample.Counterparty contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsMdmId).First().Identifier);
            var savedEntity = new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Counterparty>(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}

