using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Validation;

    public class PersonValidator : Validator<Person>
    {
        public PersonValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Person, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Person>(p => !string.IsNullOrWhiteSpace(p.Details.Surname), "Surname must not be null or an empty string"));
        }
    }
}