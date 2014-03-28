namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    using Location = EnergyTrading.MDM.Location;

    public class LocationService :
        MdmService<Contracts.Sample.Location, Location, LocationMapping, Location, LocationDetails>
    {
        public LocationService(
            IValidatorEngine validatorFactory, 
            IMappingEngine mappingEngine, 
            IRepository repository, 
            ISearchCache searchCache)
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<Location> Details(Location entity)
        {
            return new List<Location> { entity };
        }

        protected override IEnumerable<LocationMapping> Mappings(Location entity)
        {
            return entity.Mappings;
        }
    }
}