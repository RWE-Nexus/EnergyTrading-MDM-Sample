namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    public partial class PartyRoleValidatorFixture
    {
        partial void AddRelatedEntities(EnergyTrading.MDM.Contracts.Sample.PartyRole contract)
        {
            contract.Party = new EntityId() { Identifier = new MdmId() { IsMdmId = true, Identifier = "1" } };
        }
    }
}
