namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;

    public class PartyRoleMapper : Mapper<EnergyTrading.MDM.PartyRole, PartyRole>
    {
        public override void Map(EnergyTrading.MDM.PartyRole source, PartyRole destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
            destination.PartyRoleType = source.PartyRoleType;
        }
    }
}