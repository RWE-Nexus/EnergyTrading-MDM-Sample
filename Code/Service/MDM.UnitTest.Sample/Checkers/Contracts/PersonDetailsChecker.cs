﻿namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Test;

    public class PersonDetailsChecker : Checker<PersonDetails>
    {
        public PersonDetailsChecker()
        {
            Compare(x => x.Forename);
            Compare(x => x.Surname);
            Compare(x => x.TelephoneNumber);
            Compare(x => x.FaxNumber);
            Compare(x => x.Role);
            Compare(x => x.Email);
        }
    }
}
