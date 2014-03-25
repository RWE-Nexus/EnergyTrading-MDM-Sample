namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Data;

    public class PartyRoleDetailsMapper : Mapper<PartyRoleDetails, MDM.PartyRoleDetails>
    {
        private readonly IRepository repository;

        public PartyRoleDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(PartyRoleDetails source, MDM.PartyRoleDetails destination)
        {
            destination.Name = source.Name;
        }
    }
}

