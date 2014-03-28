namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;

    public class PartyRoleDetailsMapper : Mapper<Sample.PartyRoleDetails, PartyRoleDetails>
    {
        private readonly IRepository repository;

        public PartyRoleDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(Sample.PartyRoleDetails source, PartyRoleDetails destination)
        {
            destination.Name = source.Name;
        }
    }
}