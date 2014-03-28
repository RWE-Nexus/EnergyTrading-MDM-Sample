namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Test;

    public class PartyDetailsChecker : Checker<PartyDetails>
    {
        public PartyDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.TelephoneNumber);
            Compare(x => x.FaxNumber);
            Compare(x => x.Role);
            Compare(x => x.IsInternal);
        }
    }
}