namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;

    public class LocationMappingMapper : Mapper<LocationMapping, MdmId>
    {
        private readonly Mapper<IEntityMapping, MdmId> mapper;

        public LocationMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(
            LocationMapping source, 
            MdmId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}