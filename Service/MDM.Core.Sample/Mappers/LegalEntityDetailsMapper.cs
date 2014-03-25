namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;

    /// <summary>
    /// Maps a <see cref="MDM.LegalEntity" /> to a <see cref="RWEST.Nexus.MDM.Contracts.LegalEntityDetails" />
    /// </summary>
    public class LegalEntityDetailsMapper : Mapper<EnergyTrading.MDM.LegalEntityDetails, LegalEntityDetails>
    {
        public override void Map(EnergyTrading.MDM.LegalEntityDetails source, LegalEntityDetails destination)
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