namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;

    public class PersonDetailsMapper :
        Mapper<PersonDetails, Contracts.Sample.PersonDetails>
    {
        public override void Map(
            PersonDetails source, 
            Contracts.Sample.PersonDetails destination)
        {
            destination.Forename = source.FirstName;
            destination.Surname = source.LastName;
            destination.TelephoneNumber = source.Phone;
            destination.FaxNumber = source.Fax;
            destination.Role = source.Role;
            destination.Email = source.Email;
        }
    }
}