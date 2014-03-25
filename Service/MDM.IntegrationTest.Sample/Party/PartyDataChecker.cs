namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public class PartyDataChecker
    {
        public void CompareContractWithEntityDetails(EnergyTrading.MDM.Contracts.Sample.Party contract, MDM.Party entity)
        {
            PartyComparer.Compare(contract, entity);
        }

        public void ConfirmEntitySaved(int id, EnergyTrading.MDM.Contracts.Sample.Party contract)
        {
            var savedEntity =
                new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Party>(id);
            contract.Identifiers.Add(new MdmId() { IsMdmId = true, Identifier = id.ToString() });

            this.CompareContractWithEntityDetails(contract, savedEntity);
        }

        public void CompareContractWithSavedEntity(EnergyTrading.MDM.Contracts.Sample.Party contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsMdmId).First().Identifier);
            var savedEntity = new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Party>(id);

            this.CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
