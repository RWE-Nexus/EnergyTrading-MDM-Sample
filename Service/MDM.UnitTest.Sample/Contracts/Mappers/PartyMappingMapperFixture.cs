namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using NUnit.Framework;

    [TestFixture]
    public class PartyMappingMapperFixture : MappingMapperFixture
    {
        [Test]
        public void Map()
        {
            this.Map<PartyMapping>();
        }
    }
}