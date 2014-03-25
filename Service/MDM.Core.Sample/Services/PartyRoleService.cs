namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    public class PartyRoleService :
        MdmService
            <Contracts.Sample.PartyRole, PartyRole, PartyRoleMapping, PartyRoleDetails, 
            Contracts.Sample.PartyRoleDetails>
    {
        public PartyRoleService(
            IValidatorEngine validatorFactory, 
            IMappingEngine mappingEngine, 
            IRepository repository, 
            ISearchCache searchCache)
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<PartyRoleDetails> Details(PartyRole entity)
        {
            return entity.Details;
        }

        protected override IEnumerable<PartyRoleMapping> Mappings(PartyRole entity)
        {
            return entity.Mappings;
        }
    }
}