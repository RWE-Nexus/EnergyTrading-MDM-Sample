﻿namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading;
    using EnergyTrading.MDM;
    using EnergyTrading.Test;

    public class DateRangeChecker : Checker<DateRange>
    {
        public DateRangeChecker()
        {
            Compare(x => x.Start);
            Compare(x => x.Finish);
        }
    }
}