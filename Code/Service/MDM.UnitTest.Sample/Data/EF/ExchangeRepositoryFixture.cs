namespace EnergyTrading.MDM.Test.Data.EF
{
    using NUnit.Framework;

    using EnergyTrading.MDM;

    [TestFixture]
    public class ExchangeRepositoryFixture : DbSetRepositoryFixture<Exchange>
    {
        protected override Exchange Default()
        {
            var entity = ObjectMother.Create<Exchange>();

            return entity;
        }
    }
}
