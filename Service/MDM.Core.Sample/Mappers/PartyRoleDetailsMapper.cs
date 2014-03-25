namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;

    public class PartyRoleDetailsMapper :
        Mapper<PartyRoleDetails, Contracts.Sample.PartyRoleDetails>
    {
        public override void Map(
            PartyRoleDetails source, 
            Contracts.Sample.PartyRoleDetails destination)
        {
            destination.Name = source.Name;
        }
    }
}