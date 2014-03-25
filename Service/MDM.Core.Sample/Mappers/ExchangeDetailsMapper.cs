namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;

    public class ExchangeDetailsMapper : Mapper<EnergyTrading.MDM.ExchangeDetails, ExchangeDetails>
    {
        public override void Map(EnergyTrading.MDM.ExchangeDetails source, ExchangeDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Phone = source.Phone;
        }
    }
}

