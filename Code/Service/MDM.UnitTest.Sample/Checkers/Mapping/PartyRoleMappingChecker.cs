namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading.MDM;
    using EnergyTrading.Test;

    public class PartyRoleMappingChecker : Checker<PartyRoleMapping>
    {
        public PartyRoleMappingChecker()
        {
            Compare(x => x.PartyRole).Id();
        }
    }
}