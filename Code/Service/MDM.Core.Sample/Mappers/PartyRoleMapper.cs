namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class PartyRoleMapper : Mapper<PartyRole, Contracts.Sample.PartyRole>
    {
        public override void Map(
            PartyRole source, 
            Contracts.Sample.PartyRole destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
            destination.PartyRoleType = source.PartyRoleType;
        }
    }
}