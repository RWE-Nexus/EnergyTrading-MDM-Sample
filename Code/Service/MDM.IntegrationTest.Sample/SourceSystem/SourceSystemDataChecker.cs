namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public class SourceSystemDataChecker
    {
        public void CompareContractWithEntityDetails(EnergyTrading.Mdm.Contracts.SourceSystem contract, MDM.SourceSystem entity)
        {
            SourceSystemComparer.Compare(contract, entity);
        }

        public void ConfirmEntitySaved(int id, EnergyTrading.Mdm.Contracts.SourceSystem contract)
        {
            var savedEntity =
                new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.SourceSystem>(id);
            contract.Identifiers.Add(new MdmId() { IsMdmId = true, Identifier = id.ToString() });

            this.CompareContractWithEntityDetails(contract, savedEntity);
        }

        public void CompareContractWithSavedEntity(EnergyTrading.Mdm.Contracts.SourceSystem contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsMdmId).First().Identifier);
            var savedEntity = new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.SourceSystem>(id);

            this.CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
