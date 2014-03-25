namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public class PersonDataChecker
    {
        public void CompareContractWithEntityDetails(EnergyTrading.MDM.Contracts.Sample.Person contract, MDM.Person entity)
        {
            PersonComparer.Compare(contract, entity);
        }

        public void ConfirmEntitySaved(int id, EnergyTrading.MDM.Contracts.Sample.Person contract)
        {
            var savedEntity =
                new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Person>(id);
            contract.Identifiers.Add(new MdmId() { IsMdmId = true, Identifier = id.ToString() });

            this.CompareContractWithEntityDetails(contract, savedEntity);
        }

        public void CompareContractWithSavedEntity(EnergyTrading.MDM.Contracts.Sample.Person contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsMdmId).First().Identifier);
            var savedEntity = new DbSetRepository(new DbContextProvider(() => new SampleMappingContext())).FindOne<MDM.Person>(id);

            this.CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
