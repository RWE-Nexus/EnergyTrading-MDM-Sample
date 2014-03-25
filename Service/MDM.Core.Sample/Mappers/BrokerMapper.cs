namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;

    public class BrokerMapper : Mapper<EnergyTrading.MDM.Broker, Broker>
    {
        public override void Map(EnergyTrading.MDM.Broker source, Broker destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}
