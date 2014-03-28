namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    public class PersonService :
        MdmService<Contracts.Sample.Person, Person, PersonMapping, PersonDetails, Contracts.Sample.PersonDetails>
    {
        public PersonService(
            IValidatorEngine validatorFactory, 
            IMappingEngine mappingEngine, 
            IRepository repository, 
            ISearchCache searchCache)
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<PersonDetails> Details(Person entity)
        {
            return entity.Details;
        }

        protected override IEnumerable<PersonMapping> Mappings(Person entity)
        {
            return entity.Mappings;
        }
    }
}