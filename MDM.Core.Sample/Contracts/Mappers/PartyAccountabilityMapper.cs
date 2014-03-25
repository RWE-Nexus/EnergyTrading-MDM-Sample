namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Data;

    public class PartyAccountabilityMapper : ContractMapper<PartyAccountability, MDM.PartyAccountability, PartyAccountabilityDetails, MDM.PartyAccountability, PartyAccountabilityMapping>
    {
        private readonly IRepository repository;

        public PartyAccountabilityMapper(IMappingEngine mappingEngine, IRepository repository) : base(mappingEngine)
        {
            this.repository = repository;
        }

        protected override PartyAccountabilityDetails ContractDetails(PartyAccountability contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(PartyAccountability contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(PartyAccountability contract)
        {
            return contract.Identifiers;
        }
    }
}

