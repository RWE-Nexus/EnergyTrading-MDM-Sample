namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class PartyRoleDataChecker
    {
        public static void CompareContractWithEntityDetails(OpenNexus.MDM.Contracts.PartyRole contract, MDM.PartyRole entity)
        {
            MDM.PartyRoleDetails detailsToCompare = entity.Details[0]; 

            if (contract.MdmSystemData != null)
            {
                detailsToCompare = entity.Details.Where(details => details.Validity.Start == contract.MdmSystemData.StartDate).First();
            }

            Assert.AreEqual(contract.Details.Name, detailsToCompare.Name);
        }

        public static void ConfirmEntitySaved(int id, OpenNexus.MDM.Contracts.PartyRole contract)
        {
            var savedEntity =
                new DbSetRepository(new DbContextProvider(() => new NexusMappingContext())).FindOne<MDM.PartyRole>(id);
            contract.Identifiers.Add(new MdmId() { IsMdmId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(OpenNexus.MDM.Contracts.PartyRole contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsMdmId).First().Identifier);
            var savedEntity = new DbSetRepository(new DbContextProvider(() => new NexusMappingContext())).FindOne<MDM.PartyRole>(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}

