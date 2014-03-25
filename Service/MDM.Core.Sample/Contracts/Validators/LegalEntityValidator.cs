namespace EnergyTrading.MDM.Contracts.Validators
{
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Validation;

    public class LegalEntityValidator : Validator<LegalEntity>
    {
        public LegalEntityValidator(IValidatorEngine validatorEngine, IRepository repository)
        {
            Rules.Add(
                new ChildCollectionRule<LegalEntity, MdmId>(
                    validatorEngine, 
                    p => p.Identifiers));
        }
    }
}