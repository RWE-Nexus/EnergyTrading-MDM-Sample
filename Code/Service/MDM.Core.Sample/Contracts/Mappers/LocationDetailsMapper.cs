namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Data;

    using Location = EnergyTrading.MDM.Location;

    public class LocationDetailsMapper : Mapper<LocationDetails, Location>
    {
        private readonly IRepository repository;

        public LocationDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(LocationDetails source, Location destination)
        {
            destination.Name = source.Name;
            var referenceData = this.repository.LocationTypeByName(source.Type);

            // TODO: Raise an exception because this location type doesn't exist?
            destination.Type = referenceData != null ? referenceData.Value : null;
            destination.Parent = this.repository.FindEntityByMapping<Location, LocationMapping>(source.Parent);
        }
    }
}