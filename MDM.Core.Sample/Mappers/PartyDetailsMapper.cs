namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;

    public class PartyDetailsMapper : Mapper<EnergyTrading.MDM.PartyDetails, PartyDetails>
    {
        public override void Map(EnergyTrading.MDM.PartyDetails source, PartyDetails destination)
        {
            destination.Name = source.Name;
            destination.TelephoneNumber = source.Phone;
            destination.FaxNumber = source.Fax;
            destination.Role = source.Role;
            destination.IsInternal = source.IsInternal;
        }
    }
}