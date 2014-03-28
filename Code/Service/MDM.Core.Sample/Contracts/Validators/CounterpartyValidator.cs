namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Validation;

    using Party = EnergyTrading.MDM.Party;

    public class CounterpartyValidator : Validator<Counterparty>
    {
        public CounterpartyValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(
                new ChildCollectionRule<Counterparty, MdmId>(
                    validatorEngine, 
                    p => p.Identifiers));
            Rules.Add(
                new PredicateRule<Counterparty>(
                    p => !string.IsNullOrWhiteSpace(p.Details.Name), 
                    "Name must not be null or an empty string"));
            Rules.Add(
                new NexusEntityExistsRule<Counterparty, Party, PartyMapping>(repository, x => x.Party, true));
        }
    }
}