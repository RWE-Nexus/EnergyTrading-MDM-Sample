namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="MDM.Counterparty" /> to a <see cref="RWEST.Nexus.MDM.Contracts.CounterpartyDetails" />
    /// </summary>
    public class CounterpartyDetailsMapper : Mapper<EnergyTrading.MDM.CounterpartyDetails, CounterpartyDetails>
    {
        public override void Map(EnergyTrading.MDM.CounterpartyDetails source, CounterpartyDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Phone = source.Phone;
            destination.ShortName = source.ShortName;
        }
    }
}
