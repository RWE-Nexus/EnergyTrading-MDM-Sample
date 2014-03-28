namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Validation;

    using Party = EnergyTrading.MDM.Party;

    public class BrokerValidator : Validator<Broker>
    {
        public BrokerValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(
                new ChildCollectionRule<Broker, MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(
                new PredicateRule<Broker>(
                    p => !string.IsNullOrWhiteSpace(p.Details.Name), 
                    "Name must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<Broker, Party, PartyMapping>(repository, x => x.Party, true));
        }
    }
}