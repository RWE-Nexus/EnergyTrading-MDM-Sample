namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    public partial class CounterpartyValidatorFixture
    {
        partial void AddRelatedEntities(EnergyTrading.MDM.Contracts.Sample.Counterparty contract)
        {
            contract.Party = new EntityId() { Identifier = new MdmId() { IsMdmId = true, Identifier = "1" } };
        }
    }
}
