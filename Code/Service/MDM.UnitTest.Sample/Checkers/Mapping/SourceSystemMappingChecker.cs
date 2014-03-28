namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading.MDM;
    using EnergyTrading.Test;

    public class SourceSystemMappingChecker : Checker<SourceSystemMapping>
    {
        public SourceSystemMappingChecker()
        {
            Compare(x => x.SourceSystem).Id();
        }
    }
}
