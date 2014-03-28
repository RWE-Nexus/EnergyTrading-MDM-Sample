namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading.MDM;
    using EnergyTrading.Test;

    public class PersonMappingChecker : Checker<PersonMapping>
    {
        public PersonMappingChecker()
        {
            Compare(x => x.Person).Id();
        }
    }
}