namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    public class BusinessUnitService : MdmService<Contracts.Sample.BusinessUnit, BusinessUnit, PartyRoleMapping, BusinessUnitDetails, Contracts.Sample.BusinessUnitDetails>
    {

        public BusinessUnitService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache)
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<BusinessUnitDetails> Details(BusinessUnit entity)
        {
            return new List<BusinessUnitDetails>(entity.Details.Select(x => x as BusinessUnitDetails));
        }

        protected override IEnumerable<PartyRoleMapping> Mappings(BusinessUnit entity)
        {
            return entity.Mappings;
        }
    }
}