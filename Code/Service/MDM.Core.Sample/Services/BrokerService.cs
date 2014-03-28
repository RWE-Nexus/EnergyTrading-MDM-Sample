namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    public class BrokerService :
        MdmService<Contracts.Sample.Broker, Broker, PartyRoleMapping, BrokerDetails, Contracts.Sample.BrokerDetails>
    {
        public BrokerService(
            IValidatorEngine validatorFactory, 
            IMappingEngine mappingEngine, 
            IRepository repository, 
            ISearchCache searchCache)
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<BrokerDetails> Details(Broker entity)
        {
            return new List<BrokerDetails>(entity.Details.Select(x => x as BrokerDetails));
        }

        protected override IEnumerable<PartyRoleMapping> Mappings(Broker entity)
        {
            return entity.Mappings;
        }
    }
}