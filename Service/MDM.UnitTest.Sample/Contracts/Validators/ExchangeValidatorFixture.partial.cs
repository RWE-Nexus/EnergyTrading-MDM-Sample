namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    public partial class ExchangeValidatorFixture
    {
        partial void AddRelatedEntities(EnergyTrading.MDM.Contracts.Sample.Exchange contract)
        {
            contract.Party = new EntityId() { Identifier = new MdmId() { IsMdmId = true, Identifier = "1" } };
        }
    }
}
