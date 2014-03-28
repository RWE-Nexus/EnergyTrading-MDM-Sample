namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class PartyRoleChecker : Checker<EnergyTrading.MDM.Contracts.Sample.PartyRole>
    {
        public PartyRoleChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.MdmSystemData); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}
