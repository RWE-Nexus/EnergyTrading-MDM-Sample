namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class ExchangeMapper : Mapper<Exchange, Contracts.Sample.Exchange>
    {
        public override void Map(
            Exchange source, 
            Contracts.Sample.Exchange destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}