﻿namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;

    public class PartyRoleAccountabilityMapper : ContractMapper<PartyRoleAccountability, MDM.PartyRoleAccountability, PartyRoleAccountabilityDetails, MDM.PartyRoleAccountability, PartyRoleAccountabilityMapping>
    {
        private readonly IRepository repository;

        public PartyRoleAccountabilityMapper(IMappingEngine mappingEngine, IRepository repository)
            : base(mappingEngine)
        {
            this.repository = repository;
        }

        protected override PartyRoleAccountabilityDetails ContractDetails(PartyRoleAccountability contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(PartyRoleAccountability contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(PartyRoleAccountability contract)
        {
            return contract.Identifiers;
        }
    }
}

