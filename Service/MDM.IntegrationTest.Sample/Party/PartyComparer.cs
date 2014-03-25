namespace EnergyTrading.MDM.Test
{
    using System.Linq;

    using NUnit.Framework;

    public static class PartyComparer
    {
        public static void Compare(EnergyTrading.MDM.Contracts.Sample.Party contract, MDM.Party entity)
        {
            PartyDetails detailsToCompare = entity.Details[0]; 

            if (contract.MdmSystemData != null)
            {
                detailsToCompare = entity.Details.Where(details => details.Validity.Start == contract.MdmSystemData.StartDate).First();
            }

            Assert.AreEqual(contract.Details.Name, detailsToCompare.Name);
            Assert.AreEqual(contract.Details.FaxNumber, detailsToCompare.Fax);
            Assert.AreEqual(contract.Details.TelephoneNumber, detailsToCompare.Phone);
            Assert.AreEqual(contract.Details.Role, detailsToCompare.Role);
        }
    }
}