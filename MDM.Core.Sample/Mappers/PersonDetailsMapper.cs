namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample;

    public class PersonDetailsMapper : Mapper<EnergyTrading.MDM.PersonDetails, PersonDetails>
    {
        public override void Map(EnergyTrading.MDM.PersonDetails source, PersonDetails destination)
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