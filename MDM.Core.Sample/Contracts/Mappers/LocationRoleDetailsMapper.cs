namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;

    public class LocationRoleDetailsMapper : Mapper<LocationRoleDetails, MDM.LocationRole>
    {
        private readonly IRepository repository;

        public LocationRoleDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(LocationRoleDetails source, MDM.LocationRole destination)
        {
            destination.Type = this.repository.LocationRoleTypeByName(source.Type);
            destination.Location = this.repository.FindEntityByMapping<MDM.Location, LocationMapping>(source.Location);
        }
    }
}