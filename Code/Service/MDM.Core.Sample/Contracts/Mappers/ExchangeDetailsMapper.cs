namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;

    public class ExchangeDetailsMapper : Mapper<Sample.ExchangeDetails, ExchangeDetails>
    {
        private readonly IRepository repository;

        public ExchangeDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(Sample.ExchangeDetails source, ExchangeDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Phone = source.Phone;
        }
    }
}