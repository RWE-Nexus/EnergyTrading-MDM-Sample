namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;

    public class ExchangeDetailsMapper :
        Mapper<ExchangeDetails, Contracts.Sample.ExchangeDetails>
    {
        public override void Map(
            ExchangeDetails source, 
            Contracts.Sample.ExchangeDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Phone = source.Phone;
        }
    }
}