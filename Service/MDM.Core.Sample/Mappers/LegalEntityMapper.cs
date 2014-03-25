namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;

    public class LegalEntityMapper : Mapper<EnergyTrading.MDM.LegalEntity, LegalEntity>
    {
        public override void Map(EnergyTrading.MDM.LegalEntity source, LegalEntity destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}
