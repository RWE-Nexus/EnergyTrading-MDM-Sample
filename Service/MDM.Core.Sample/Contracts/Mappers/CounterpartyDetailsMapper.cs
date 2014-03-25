namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Data;

    /// <summary>
	///
	/// </summary>
    public class CounterpartyDetailsMapper : Mapper<CounterpartyDetails, MDM.CounterpartyDetails>
    {
        private IRepository repository;

        public CounterpartyDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(CounterpartyDetails source, MDM.CounterpartyDetails destination)
        {
            destination.Phone = source.Phone;
            destination.Fax = source.Fax;
            destination.Name = source.Name;
            destination.ShortName = source.ShortName;
        }
    }
}
