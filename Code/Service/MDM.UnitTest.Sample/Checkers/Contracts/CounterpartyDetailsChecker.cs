namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Test;

    public class CounterpartyDetailsChecker : Checker<EnergyTrading.MDM.Contracts.Sample.CounterpartyDetails>
    {
        public CounterpartyDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Phone);
            Compare(x => x.Fax);
            Compare(x => x.ShortName);
        }
    }
}
