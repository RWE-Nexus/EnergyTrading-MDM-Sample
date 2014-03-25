namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using NUnit.Framework;

    using EnergyTrading.MDM;

    [TestFixture]
    public class LocationMappingRepositoryFixture : DbSetRepositoryFixture<LocationMapping>
    {
        protected override LocationMapping Default()
        {
            var entity = base.Default();
            entity.Location = ObjectMother.Create<Location>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
