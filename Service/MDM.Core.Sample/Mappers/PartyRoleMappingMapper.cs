namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;

    public class PartyRoleMappingMapper : Mapper<PartyRoleMapping, MdmId>
    {
        private readonly Mapper<IEntityMapping, MdmId> mapper;

        public PartyRoleMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(
            PartyRoleMapping source, 
            MdmId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}