namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class LegalEntityMapper :
        Mapper<LegalEntity, Contracts.Sample.LegalEntity>
    {
        public override void Map(
            LegalEntity source, 
            Contracts.Sample.LegalEntity destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}