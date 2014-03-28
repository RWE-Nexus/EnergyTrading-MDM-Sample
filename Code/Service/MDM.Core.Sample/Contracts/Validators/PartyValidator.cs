namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Validation;

    public class PartyValidator : Validator<Party>
    {
        public PartyValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(
                new ChildCollectionRule<Party, MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(
                new PredicateRule<Party>(
                    p => !string.IsNullOrWhiteSpace(p.Details.Name), 
                    "Name must not be null or an empty string"));

            // Rules.Add(new EntityNoOverlappingRule<Party>(repository, p=>p.ToMdmKey(), p => p.Details.Name, p => p.Nexus.StartDate, p => p.Nexus.EndDate));
        }
    }
}