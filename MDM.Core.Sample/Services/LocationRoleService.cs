namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    using LocationRole = EnergyTrading.MDM.LocationRole;

    public class LocationRoleService : MdmService<Contracts.Sample.LocationRole, LocationRole, LocationRoleMapping, LocationRole, LocationRoleDetails>
    {
        public LocationRoleService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<LocationRole> Details(LocationRole entity)
        {
            return new List<LocationRole> { entity };
        }

        protected override IEnumerable<LocationRoleMapping> Mappings(LocationRole entity)
        {
            return entity.Mappings;
        }
    }
}