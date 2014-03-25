namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    using Unit = EnergyTrading.MDM.Unit;

    public class UnitService : MdmService<Contracts.Sample.Unit, Unit, UnitMapping, Unit, UnitDetails>
	    {

    public UnitService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<Unit> Details(Unit entity)
        {
			return new List<Unit> { entity };
	        }

        protected override IEnumerable<UnitMapping> Mappings(Unit entity)
        {
            return entity.Mappings;
        }
    }
}