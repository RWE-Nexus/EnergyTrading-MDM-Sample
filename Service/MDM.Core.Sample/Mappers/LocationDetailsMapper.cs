namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;

    using Location = EnergyTrading.MDM.Location;

    public class LocationDetailsMapper :
        Mapper<Location, LocationDetails>
    {
        public override void Map(
            Location source, 
            LocationDetails destination)
        {
            destination.Type = source.Type ?? string.Empty;
            destination.Name = source.Name;
            destination.Parent = source.Parent.CreateNexusEntityId(() => source.Parent.Name);
        }
    }
}