namespace EnergyTrading.MDM.Test.Data.EF
{
    using NUnit.Framework;

    using EnergyTrading.MDM;

    [TestFixture]
    public class CounterpartyRepositoryFixture : DbSetRepositoryFixture<Counterparty>
    {
        protected override Counterparty Default()
        {
            var entity = ObjectMother.Create<Counterparty>();

            return entity;
        }
    }
}
