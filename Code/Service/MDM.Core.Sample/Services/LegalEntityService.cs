namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    public class LegalEntityService :
        MdmService
            <Contracts.Sample.LegalEntity, LegalEntity, PartyRoleMapping, LegalEntityDetails, 
            Contracts.Sample.LegalEntityDetails>
    {
        public LegalEntityService(
            IValidatorEngine validatorFactory, 
            IMappingEngine mappingEngine, 
            IRepository repository, 
            ISearchCache searchCache)
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<LegalEntityDetails> Details(LegalEntity entity)
        {
            return new List<LegalEntityDetails>(entity.Details.OfType<LegalEntityDetails>());
        }

        protected override IEnumerable<PartyRoleMapping> Mappings(LegalEntity entity)
        {
            return entity.Mappings;
        }
    }
}