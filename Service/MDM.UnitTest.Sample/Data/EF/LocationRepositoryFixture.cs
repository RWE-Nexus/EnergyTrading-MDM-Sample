namespace EnergyTrading.MDM.Test.Data.EF
{
    using NUnit.Framework;

    using EnergyTrading.MDM;

    [TestFixture]
    public class LocationRepositoryFixture : DbSetRepositoryFixture<Location>
    {
        protected override Location Default()
        {
            var entity = ObjectMother.Create<Location>();

            return entity;
        }
    }
}
