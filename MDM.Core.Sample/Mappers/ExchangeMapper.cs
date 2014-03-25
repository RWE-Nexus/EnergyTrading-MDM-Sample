namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;

    public class ExchangeMapper : Mapper<EnergyTrading.MDM.Exchange, Exchange>
    {
        public override void Map(EnergyTrading.MDM.Exchange source, Exchange destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}
