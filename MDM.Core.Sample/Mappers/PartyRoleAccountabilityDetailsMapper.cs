namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="MDM.PartyRoleAccountability" /> to a <see cref="RWEST.Nexus.MDM.Contracts.PartyRoleAccountabilityDetails" />
    /// </summary>
    public class PartyRoleAccountabilityDetailsMapper : Mapper<EnergyTrading.MDM.PartyRoleAccountability, PartyRoleAccountabilityDetails>
    {
        public override void Map(EnergyTrading.MDM.PartyRoleAccountability source, PartyRoleAccountabilityDetails destination)
        {
            destination.Name = source.Name;
            destination.SourcePartyRole = source.SourcePartyRole.CreateNexusEntityId(() => source.SourcePartyRole.LatestDetails.Name);
            destination.TargetPartyRole = source.TargetPartyRole.CreateNexusEntityId(() => source.TargetPartyRole.LatestDetails.Name);
            destination.PartyRoleAccountabilityType = source.PartyRoleAccountabilityType;
        }
    }
}