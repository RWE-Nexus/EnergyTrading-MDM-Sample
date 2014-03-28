namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using EnergyTrading.MDM;
    using EnergyTrading.Test;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    public class BrokerDetailsChecker : Checker<EnergyTrading.MDM.Contracts.Sample.BrokerDetails>
    {
        public BrokerDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Phone);
            Compare(x => x.Fax);
            Compare(x => x.Rate);
        }
    }
}
