namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading.MDM;
    using EnergyTrading.Test;

    public class PartyChecker : Checker<Party>
    {
        public PartyChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Details).Count();
            Compare(x => x.Mappings).Count();
        }
    }
}
