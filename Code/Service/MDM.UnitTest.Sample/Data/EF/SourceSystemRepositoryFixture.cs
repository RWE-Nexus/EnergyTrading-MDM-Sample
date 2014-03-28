namespace EnergyTrading.MDM.Test.Data.EF
{
    using NUnit.Framework;

    using EnergyTrading.MDM;

    [TestFixture]
    public class SourceSystemRepositoryFixture : DbSetRepositoryFixture<SourceSystem>
    {
        protected override SourceSystem Default()
        {
            var entity = ObjectMother.Create<SourceSystem>();

            return entity;
        }
    }
}
