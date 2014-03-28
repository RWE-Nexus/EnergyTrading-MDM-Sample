namespace EnergyTrading.MDM.Test.Data.EF
{
    using NUnit.Framework;

    using EnergyTrading.MDM;

    [TestFixture]
    public class PartyRoleRepositoryFixture : DbSetRepositoryFixture<PartyRole>
    {
        protected override PartyRole Default()
        {
            var entity = ObjectMother.Create<PartyRole>();

            return entity;
        }
    }
}