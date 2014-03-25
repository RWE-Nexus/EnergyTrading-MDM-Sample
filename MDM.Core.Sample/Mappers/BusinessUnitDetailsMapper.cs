namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="MDM.BusinessUnit" /> to a <see cref="RWEST.Nexus.MDM.Contracts.BusinessUnitDetails" />
    /// </summary>
    public class BusinessUnitDetailsMapper : Mapper<EnergyTrading.MDM.BusinessUnitDetails, BusinessUnitDetails>
    {
        public override void Map(EnergyTrading.MDM.BusinessUnitDetails source, BusinessUnitDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Phone = source.Phone;
            destination.AccountType = source.AccountType;
            destination.Address = source.Address;
            destination.Status = source.Status;
            destination.TaxLocation = source.TaxLocation.CreateNexusEntityId(() => source.TaxLocation.Name);
        }
    }
}