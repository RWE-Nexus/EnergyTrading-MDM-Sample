namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class CounterpartyMapper :
        Mapper<Counterparty, Contracts.Sample.Counterparty>
    {
        public override void Map(
            Counterparty source, 
            Contracts.Sample.Counterparty destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}