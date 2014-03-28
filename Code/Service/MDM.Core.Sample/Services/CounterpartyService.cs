namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    public class CounterpartyService :
        MdmService
            <Contracts.Sample.Counterparty, Counterparty, PartyRoleMapping, CounterpartyDetails, 
            Contracts.Sample.CounterpartyDetails>
    {
        public CounterpartyService(
            IValidatorEngine validatorFactory, 
            IMappingEngine mappingEngine, 
            IRepository repository, 
            ISearchCache searchCache)
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<CounterpartyDetails> Details(Counterparty entity)
        {
            return new List<CounterpartyDetails>(entity.Details.Select(x => x as CounterpartyDetails));
        }

        protected override IEnumerable<PartyRoleMapping> Mappings(Counterparty entity)
        {
            return entity.Mappings;
        }
    }
}