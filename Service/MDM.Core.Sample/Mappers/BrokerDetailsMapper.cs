namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="MDM.Broker" /> to a <see cref="RWEST.Nexus.MDM.Contracts.BrokerDetails" />
    /// </summary>
    public class BrokerDetailsMapper : Mapper<EnergyTrading.MDM.BrokerDetails, BrokerDetails>
    {
        public override void Map(EnergyTrading.MDM.BrokerDetails source, BrokerDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Rate = source.Rate;
            destination.Phone = source.Phone;
        }
    }
}