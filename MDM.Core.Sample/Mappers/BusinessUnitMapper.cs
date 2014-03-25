namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Mapping;

    public class BusinessUnitMapper : Mapper<EnergyTrading.MDM.BusinessUnit, BusinessUnit>
    {
        public override void Map(EnergyTrading.MDM.BusinessUnit source, BusinessUnit destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}