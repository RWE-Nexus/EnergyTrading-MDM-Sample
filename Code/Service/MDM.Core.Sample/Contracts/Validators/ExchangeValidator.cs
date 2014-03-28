namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Validation;

    using Party = EnergyTrading.MDM.Party;

    public class ExchangeValidator : Validator<Exchange>
    {
        public ExchangeValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(
                new ChildCollectionRule<Exchange, MdmId>(
                    validatorEngine, 
                    p => p.Identifiers));
            Rules.Add(
                new PredicateRule<Exchange>(
                    p => !string.IsNullOrWhiteSpace(p.Details.Name), 
                    "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<Exchange, Party, PartyMapping>(repository, x => x.Party, true));
        }
    }
}