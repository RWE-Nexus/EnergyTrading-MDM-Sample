namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class ExchangeDataChecker
    {
        public static void CompareContractWithEntityDetails(EnergyTrading.MDM.Contracts.Sample.Exchange contract, MDM.Exchange entity)
        {
            MDM.ExchangeDetails correctDetail = null;

            if (entity.Details.Count == 1)
            {
                correctDetail = entity.Details[0] as MDM.ExchangeDetails;
            }
            else
            {
                correctDetail = (MDM.ExchangeDetails)entity.Details.Where(
                    x => x.Validity.Start >= contract.MdmSystemData.StartDate && x.Validity.Finish >= contract.MdmSystemData.EndDate).First();
            }

            Assert.AreEqual(contract.Details.Name, correctDetail.Name);
            Assert.AreEqual(contract.Details.Phone, correctDetail.Phone);
            Assert.AreEqual(contract.Details.Fax, correctDetail.Fax);
            Assert.AreEqual(contract.Party.MdmId(), entity.Party.Id);
        }

        public static void ConfirmEntitySaved(int id, EnergyTrading.MDM.Contracts.Sample.Exchange contract)
        {
            var savedEntity =
                new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Exchange>(id);
            contract.Identifiers.Add(new MdmId() { IsMdmId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(EnergyTrading.MDM.Contracts.Sample.Exchange contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsMdmId).First().Identifier);
            var savedEntity = new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Exchange>(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
