namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;

    /// <summary>
    /// Maps a <see cref="MDM.Counterparty" /> to a <see cref="RWEST.Nexus.MDM.Contracts.CounterpartyDetails" />
    /// </summary>
    public class CounterpartyDetailsMapper :
        Mapper<CounterpartyDetails, Contracts.Sample.CounterpartyDetails>
    {
        public override void Map(
            CounterpartyDetails source, 
            Contracts.Sample.CounterpartyDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Phone = source.Phone;
            destination.ShortName = source.ShortName;
        }
    }
}