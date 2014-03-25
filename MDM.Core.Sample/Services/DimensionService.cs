namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    using Dimension = EnergyTrading.MDM.Dimension;

    public class DimensionService : MdmService<Contracts.Sample.Dimension, Dimension, DimensionMapping, Dimension, DimensionDetails>
	    {

    public DimensionService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<Dimension> Details(Dimension entity)
        {
			return new List<Dimension> { entity };
	        }

        protected override IEnumerable<DimensionMapping> Mappings(Dimension entity)
        {
            return entity.Mappings;
        }
    }
}