﻿namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Data;

    public class PartyRoleAccountabilityDetailsMapper : Mapper<PartyRoleAccountabilityDetails, MDM.PartyRoleAccountability>
    {
        private readonly IRepository repository;

        public PartyRoleAccountabilityDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(PartyRoleAccountabilityDetails source, MDM.PartyRoleAccountability destination)
        {
            destination.Name = source.Name;
            destination.PartyRoleAccountabilityType = source.PartyRoleAccountabilityType;
            destination.SourcePartyRole = this.repository.FindEntityByMapping<MDM.PartyRole, PartyRoleMapping>(source.SourcePartyRole);
            destination.TargetPartyRole = this.repository.FindEntityByMapping<MDM.PartyRole, PartyRoleMapping>(source.TargetPartyRole);
        }
    }
}