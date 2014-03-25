namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;

    public class PartyRoleDetailsMapper : Mapper<EnergyTrading.MDM.PartyRoleDetails, PartyRoleDetails>
    {
        public override void Map(EnergyTrading.MDM.PartyRoleDetails source, PartyRoleDetails destination)
        {
            destination.Name = source.Name;
        }
    }
}
