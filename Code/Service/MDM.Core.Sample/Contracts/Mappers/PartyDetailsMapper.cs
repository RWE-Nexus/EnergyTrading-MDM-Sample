namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Mapping;

    public class PartyDetailsMapper : Mapper<Sample.PartyDetails, PartyDetails>
    {
        public override void Map(Sample.PartyDetails source, PartyDetails destination)
        {
            destination.Name = source.Name;
            destination.Phone = source.TelephoneNumber;
            destination.Fax = source.FaxNumber;
            destination.Role = source.Role;
            destination.IsInternal = source.IsInternal;
        }
    }
}