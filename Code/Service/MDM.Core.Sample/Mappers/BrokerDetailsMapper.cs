namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;

    /// <summary>
    /// Maps a <see cref="MDM.Broker" /> to a <see cref="Contracts.Sample.BrokerDetails" />
    /// </summary>
    public class BrokerDetailsMapper : Mapper<BrokerDetails, Contracts.Sample.BrokerDetails>
    {
        public override void Map(BrokerDetails source, Contracts.Sample.BrokerDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Rate = source.Rate;
            destination.Phone = source.Phone;
        }
    }
}