namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    using ExchangeDetails = EnergyTrading.MDM.Contracts.Sample.ExchangeDetails;

    public class ExchangeDetailsChecker : Checker<ExchangeDetails>
    {
        public ExchangeDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Phone);
            Compare(x => x.Fax);
        }
    }
}
