namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Sample;

    using DateRange = EnergyTrading.DateRange;
    using Location = EnergyTrading.MDM.Location;

    /// <summary>
    /// Maps a <see cref="MDM.SourceSystem" /> to a <see cref="MDM.Location" />
    /// </summary>
    public class LocationMapper :
        ContractMapper<Sample.Location, Location, LocationDetails, Location, LocationMapping>
    {
        public LocationMapper(IMappingEngine mappingEngine)
            : base(mappingEngine)
        {
        }

        protected override LocationDetails ContractDetails(Sample.Location contract)
        {
            return contract.Details;
        }

        protected override DateRange ContractDetailsValidity(Sample.Location contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<MdmId> Identifiers(Sample.Location contract)
        {
            return contract.Identifiers;
        }
    }
}