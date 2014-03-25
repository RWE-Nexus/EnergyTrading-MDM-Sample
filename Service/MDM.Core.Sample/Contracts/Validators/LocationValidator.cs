namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Validation;

    using Location = EnergyTrading.MDM.Contracts.Sample.Location;

    public class LocationValidator : Validator<Location>
    {
        public LocationValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(
                new ChildCollectionRule<Location, MdmId>(
                    validatorEngine, 
                    p => p.Identifiers));
            Rules.Add(
                new PredicateRule<Location>(
                    p => !string.IsNullOrWhiteSpace(p.Details.Name), 
                    "Name must not be null or an empty string"));
            Rules.Add(
                new NexusEntityExistsRule<Location, MDM.Location, LocationMapping>(
                    repository, 
                    x => x.Details.Parent, 
                    false));
            Rules.Add(
                new ParentDiffersRule<Location, MDM.Location, LocationMapping>(
                    repository, 
                    x => x.Details.Name, 
                    x => x.Details.Parent, 
                    y => y.Name));
        }
    }
}