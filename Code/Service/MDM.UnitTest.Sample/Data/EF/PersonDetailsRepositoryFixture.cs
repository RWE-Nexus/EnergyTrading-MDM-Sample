namespace EnergyTrading.MDM.Test.Data.EF
{
    using NUnit.Framework;

    using EnergyTrading.MDM;

    [TestFixture]
    public class PersonDetailsRepositoryFixture : DbSetRepositoryFixture<PersonDetails>
    {
        protected override PersonDetails Default()
        {
            var entity = base.Default();
            entity.Person = new Person();

            return entity;
        }
    }
}