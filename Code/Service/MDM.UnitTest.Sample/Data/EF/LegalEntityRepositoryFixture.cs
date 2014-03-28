namespace EnergyTrading.MDM.Test.Data.EF
{
    using NUnit.Framework;

    using EnergyTrading.MDM;

    [TestFixture]
    public class LegalEntityRepositoryFixture : DbSetRepositoryFixture<LegalEntity>
    {
        protected override LegalEntity Default()
        {
            var entity = ObjectMother.Create<LegalEntity>();

            return entity;
        }
    }
}
