namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;

    public class PartyMappingMapper : Mapper<PartyMapping, MdmId>
    {
        private readonly Mapper<IEntityMapping, MdmId> mapper;

        public PartyMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(PartyMapping source, MdmId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}