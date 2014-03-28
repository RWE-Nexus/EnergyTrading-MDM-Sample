namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading.MDM;
    using EnergyTrading.Test;

    public class LocationMappingChecker : Checker<LocationMapping>
    {
        public LocationMappingChecker()
        {
            Compare(x => x.Location).Id();
        }
    }
}
