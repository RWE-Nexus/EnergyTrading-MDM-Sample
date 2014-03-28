namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class BrokerChecker : Checker<EnergyTrading.MDM.Contracts.Sample.Broker>
    {
        public BrokerChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.MdmSystemData); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}
