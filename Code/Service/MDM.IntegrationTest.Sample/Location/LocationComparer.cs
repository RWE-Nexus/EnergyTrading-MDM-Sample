namespace EnergyTrading.MDM.Test
{
    using NUnit.Framework;

    public static class LocationComparer
    {
        public static void Compare(EnergyTrading.MDM.Contracts.Sample.Location contract, Location entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.Type, entity.Type);
        }
    }
}
