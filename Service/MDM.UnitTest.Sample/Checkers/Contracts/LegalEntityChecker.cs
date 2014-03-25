namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Test;

    public class LegalEntityChecker : Checker<EnergyTrading.MDM.Contracts.Sample.LegalEntity>
    {
        public LegalEntityChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.MdmSystemData); 
            Compare(x => x.Audit);
            Compare(x => x.Links);
        }
    }
}
