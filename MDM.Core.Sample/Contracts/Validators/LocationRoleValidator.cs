namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Validation;

    public class LocationRoleValidator : Validator<LocationRole>
    {
        public LocationRoleValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<LocationRole, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
            Rules.Add(new NexusEntityExistsRule<LocationRole, MDM.Location, MDM.LocationMapping>(repository, x => x.Details.Location, true));        }
    }
}
		