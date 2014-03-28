namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading.MDM;
    using EnergyTrading.Test;

    public class PartyMappingChecker : Checker<PartyMapping>
    {
        public PartyMappingChecker()
        {
            Compare(x => x.Party).Id();
        }
   }
}
