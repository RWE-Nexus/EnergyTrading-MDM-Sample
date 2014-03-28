namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Mapping;

    public class PersonDetailsMapper : Mapper<Sample.PersonDetails, PersonDetails>
    {
        public override void Map(Sample.PersonDetails source, PersonDetails destination)
        {
            destination.FirstName = source.Forename;
            destination.LastName = source.Surname;
            destination.Phone = source.TelephoneNumber;
            destination.Fax = source.FaxNumber;
            destination.Role = source.Role;
            destination.Email = source.Email;

            // destination.Validity = source;  // NB Have to do this in the ContractMapper as it owns the validity
        }
    }
}