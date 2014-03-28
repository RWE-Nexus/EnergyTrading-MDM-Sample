namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;

    public class PersonMappingMapper : Mapper<PersonMapping, MdmId>
    {
        private readonly Mapper<IEntityMapping, MdmId> mapper;

        public PersonMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(PersonMapping source, MdmId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}