namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;

    /// <summary>
    ///
    /// </summary>
    public class BrokerDetailsMapper : Mapper<Sample.BrokerDetails, BrokerDetails>
    {
        private IRepository repository;

        public BrokerDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(Sample.BrokerDetails source, BrokerDetails destination)
        {
            destination.Phone = source.Phone;
            destination.Rate = source.Rate;
            destination.Fax = source.Fax;
            destination.Name = source.Name;
        }
    }
}