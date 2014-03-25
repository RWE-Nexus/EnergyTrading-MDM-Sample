namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;

    public class PersonDetailsMapper : Mapper<PersonDetails, MDM.PersonDetails>
    {
        public override void Map(PersonDetails source, MDM.PersonDetails destination)
        {
            destination.FirstName = source.Forename;
            destination.LastName = source.Surname;
            destination.Phone = source.TelephoneNumber;
            destination.Fax = source.FaxNumber;
            destination.Role = source.Role;
            destination.Email = source.Email;
            //destination.Validity = source;  // NB Have to do this in the ContractMapper as it owns the validity
        }
    }
}