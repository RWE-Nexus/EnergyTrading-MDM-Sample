namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    using PartyAccountability = EnergyTrading.MDM.PartyAccountability;

    public class PartyAccountabilityService : MdmService<Contracts.Sample.PartyAccountability, PartyAccountability, PartyAccountabilityMapping, PartyAccountability, PartyAccountabilityDetails>
	    {

    public PartyAccountabilityService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<PartyAccountability> Details(PartyAccountability entity)
        {
            return new List<PartyAccountability> { entity };
        }

        protected override IEnumerable<PartyAccountabilityMapping> Mappings(PartyAccountability entity)
        {
            return entity.Mappings;
        }
    }
}