namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Validation;
    using EnergyTrading.Data;

    public class LegalEntityValidator : Validator<LegalEntity>
    {
        public LegalEntityValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(new ChildCollectionRule<LegalEntity, EnergyTrading.Mdm.Contracts.MdmId>(validatorEngine, p => p.Identifiers));
        }
    }
}