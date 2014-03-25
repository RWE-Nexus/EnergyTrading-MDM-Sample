namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using EnergyTrading.MDM;
    using EnergyTrading.Test;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    using PartyRoleDetails = EnergyTrading.MDM.Contracts.Sample.PartyRoleDetails;

    public class PartyRoleDetailsChecker : Checker<PartyRoleDetails>
    {
        public PartyRoleDetailsChecker()
        {
            Compare(x => x.Name);
        }
    }
}
