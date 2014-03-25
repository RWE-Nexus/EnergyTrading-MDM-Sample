namespace EnergyTrading.MDM.Test.Data.EF
{
    using NUnit.Framework;

    using EnergyTrading.MDM;

    [TestFixture]
    public class BrokerRepositoryFixture : DbSetRepositoryFixture<Broker>
    {
        protected override Broker Default()
        {
            var entity = ObjectMother.Create<Broker>();

            return entity;
        }
    }
}
