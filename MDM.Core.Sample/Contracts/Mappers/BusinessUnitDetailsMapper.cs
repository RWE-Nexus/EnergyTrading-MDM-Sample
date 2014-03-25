namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Data;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Mapping;

    /// <summary>
	///
	/// </summary>
    public class BusinessUnitDetailsMapper : Mapper<BusinessUnitDetails, MDM.BusinessUnitDetails>
    {
        private readonly IRepository repository;

        public BusinessUnitDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(BusinessUnitDetails source, MDM.BusinessUnitDetails destination)
        {
            destination.Name = source.Name;
            destination.Phone = source.Phone;
            destination.Fax = source.Fax;
            destination.AccountType = source.AccountType;
            destination.Address = source.Address;
            destination.Status = source.Status;
            destination.TaxLocation = this.repository.FindEntityByMapping<MDM.Location, LocationMapping>(source.TaxLocation);
        }
    }
}