namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    using PartyRoleAccountability = EnergyTrading.MDM.PartyRoleAccountability;

    public class PartyRoleAccountabilityService : MdmService<Contracts.Sample.PartyRoleAccountability, PartyRoleAccountability, PartyRoleAccountabilityMapping, PartyRoleAccountability, PartyRoleAccountabilityDetails>
    {
        public PartyRoleAccountabilityService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache)
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<PartyRoleAccountability> Details(PartyRoleAccountability entity)
        {
            return new List<PartyRoleAccountability> { entity };
        }

        protected override IEnumerable<PartyRoleAccountabilityMapping> Mappings(PartyRoleAccountability entity)
        {
            return entity.Mappings;
        }
    }
}