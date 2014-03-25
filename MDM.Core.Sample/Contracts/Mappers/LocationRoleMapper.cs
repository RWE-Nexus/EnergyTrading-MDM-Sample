namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;

    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="LocationRole" />
    /// </summary>
    public class LocationRoleMapper : ContractMapper<LocationRole, MDM.LocationRole, LocationRoleDetails, MDM.LocationRole, LocationRoleMapping>
    {
        public LocationRoleMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override LocationRoleDetails ContractDetails(LocationRole contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(LocationRole contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(LocationRole contract)
        {
            return contract.Identifiers;
        }
    }
}