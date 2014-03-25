namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;

    public class PartyDetailsMapper : Mapper<PartyDetails, MDM.PartyDetails>
    {
        public override void Map(PartyDetails source, MDM.PartyDetails destination)
        {
            destination.Name = source.Name;
            destination.Phone = source.TelephoneNumber;
            destination.Fax = source.FaxNumber;
            destination.Role = source.Role;
            destination.IsInternal = source.IsInternal;
        }
    }
}
