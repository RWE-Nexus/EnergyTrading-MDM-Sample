using EnergyTrading.MDM.Contracts.Rules;

namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Validation;
	using EnergyTrading.Data;

    public class DimensionValidator : Validator<Dimension>
    {
        public DimensionValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<Dimension, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new PredicateRule<Dimension>(p => !string.IsNullOrWhiteSpace(p.Details.Name), "Name must not be null or an empty string"));
        }
    }
}