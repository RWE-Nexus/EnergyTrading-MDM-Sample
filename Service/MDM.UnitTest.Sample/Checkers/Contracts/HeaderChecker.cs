﻿namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Test;

    public class HeaderChecker : Checker<Header>
    {
        public HeaderChecker()
        {
            Compare(x => x.Notes);
            Compare(x => x.Version);
        }
    }
}