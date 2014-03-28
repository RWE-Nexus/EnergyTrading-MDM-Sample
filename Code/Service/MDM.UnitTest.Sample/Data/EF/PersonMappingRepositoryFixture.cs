namespace EnergyTrading.MDM.Test.Data.EF
{
    using NUnit.Framework;

    using EnergyTrading.MDM;

    [TestFixture]
    public class PersonMappingRepositoryFixture : DbSetRepositoryFixture<PersonMapping>
    {
        protected override PersonMapping Default()
        {
            var entity = base.Default();
            entity.Person = new Person();
            entity.System = new SourceSystem { Name = "Mapping" };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}