namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class BrokerMapper : Mapper<Broker, Contracts.Sample.Broker>
    {
        public override void Map(Broker source, Contracts.Sample.Broker destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}