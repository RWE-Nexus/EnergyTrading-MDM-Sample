namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Mapping;

    public class LegalEntityDetailsMapper : Mapper<Sample.LegalEntityDetails, LegalEntityDetails>
    {
        public override void Map(Sample.LegalEntityDetails source, LegalEntityDetails destination)
        {
            destination.Name = source.Name;
            destination.Address = source.Address;
            destination.CountryOfIncorporation = source.CountryOfIncorporation;
            destination.Email = source.Email;
            destination.Fax = source.Fax;
            destination.PartyStatus = source.PartyStatus;
            destination.Phone = source.Phone;
            destination.RegisteredName = source.RegisteredName;
            destination.RegistrationNumber = source.RegistrationNumber;
            destination.Website = source.Website;
            destination.CustomerAddress = source.CustomerAddress;
            destination.InvoiceSetup = source.InvoiceSetup;
            destination.VendorAddress = source.VendorAddress;
        }
    }
}