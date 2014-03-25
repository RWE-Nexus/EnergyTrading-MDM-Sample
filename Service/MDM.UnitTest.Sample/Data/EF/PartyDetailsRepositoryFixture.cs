namespace EnergyTrading.MDM.Test.Data.EF
{
    using NUnit.Framework;

    using EnergyTrading.MDM;

    [TestFixture]
    public class PartyDetailsRepositoryFixture : DbSetRepositoryFixture<PartyDetails>
    {
        protected override PartyDetails Default()
        {
            var entity = base.Default();
            entity.Party = new Party();

            return entity;
        }
    }
}
