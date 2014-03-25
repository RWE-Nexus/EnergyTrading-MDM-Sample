namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Validation;

    using Party = EnergyTrading.MDM.Party;

    public class PartyRoleValidator : Validator<PartyRole>
    {
        public PartyRoleValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(
                new ChildCollectionRule<PartyRole, MdmId>(
                    validatorEngine, 
                    p => p.Identifiers));
            Rules.Add(
                new PredicateRule<PartyRole>(
                    p => !string.IsNullOrWhiteSpace(p.Details.Name), 
                    "Name must not be null or an empty string"));
            Rules.Add(
                new PredicateRule<PartyRole>(
                    p => !string.IsNullOrWhiteSpace(p.PartyRoleType), 
                    "PartyRoleType must not be null or an empty string"));
            Rules.Add(new NexusEntityExistsRule<PartyRole, Party, PartyMapping>(repository, x => x.Party, true));
        }
    }
}