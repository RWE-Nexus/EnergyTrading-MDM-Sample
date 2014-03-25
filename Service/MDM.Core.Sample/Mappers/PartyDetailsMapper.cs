namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;

    public class PartyDetailsMapper :
        Mapper<PartyDetails, Contracts.Sample.PartyDetails>
    {
        public override void Map(
            PartyDetails source, 
            Contracts.Sample.PartyDetails destination)
        {
            destination.Name = source.Name;
            destination.TelephoneNumber = source.Phone;
            destination.FaxNumber = source.Fax;
            destination.Role = source.Role;
            destination.IsInternal = source.IsInternal;
        }
    }
}