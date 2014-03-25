namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;

    /// <summary>
    ///
    /// </summary>
    public class CounterpartyDetailsMapper : Mapper<Sample.CounterpartyDetails, CounterpartyDetails>
    {
        private IRepository repository;

        public CounterpartyDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(Sample.CounterpartyDetails source, CounterpartyDetails destination)
        {
            destination.Phone = source.Phone;
            destination.Fax = source.Fax;
            destination.Name = source.Name;
            destination.ShortName = source.ShortName;
        }
    }
}