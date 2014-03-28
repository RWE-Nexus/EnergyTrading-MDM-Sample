namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class ExchangeChecker : Checker<EnergyTrading.MDM.Contracts.Sample.Exchange>
    {
        public ExchangeChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.MdmSystemData); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}
