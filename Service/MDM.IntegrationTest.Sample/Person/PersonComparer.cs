namespace EnergyTrading.MDM.Test
{
    using System.Linq;

    using NUnit.Framework;

    public static class PersonComparer
    {
        public static void Compare(EnergyTrading.MDM.Contracts.Sample.Person contract, MDM.Person entity)
        {
            PersonDetails detailsToCompare = entity.Details[0]; 

            if (contract.MdmSystemData != null)
            {
                detailsToCompare = entity.Details.Where(details => details.Validity.Start == contract.MdmSystemData.StartDate).First();
            }

            Assert.AreEqual(contract.Details.Forename, detailsToCompare.FirstName);
            Assert.AreEqual(contract.Details.Surname, detailsToCompare.LastName);
            Assert.AreEqual(contract.Details.FaxNumber, detailsToCompare.Fax);
            Assert.AreEqual(contract.Details.Role, detailsToCompare.Role);
            Assert.AreEqual(contract.Details.TelephoneNumber, detailsToCompare.Phone);
            Assert.AreEqual(contract.Details.Email, detailsToCompare.Email);
        }
    }
}