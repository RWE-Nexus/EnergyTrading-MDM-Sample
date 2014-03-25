namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;

    public class CounterpartyMapper : Mapper<EnergyTrading.MDM.Counterparty, Counterparty>
    {
        public override void Map(EnergyTrading.MDM.Counterparty source, Counterparty destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}
