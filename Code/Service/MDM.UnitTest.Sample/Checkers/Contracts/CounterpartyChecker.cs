namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class CounterpartyChecker : Checker<EnergyTrading.MDM.Contracts.Sample.Counterparty>
    {
        public CounterpartyChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.MdmSystemData); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}
