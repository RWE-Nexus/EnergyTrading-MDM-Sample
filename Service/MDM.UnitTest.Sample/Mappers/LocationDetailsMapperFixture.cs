namespace EnergyTrading.MDM.Test.Mappers
{
    using NUnit.Framework;

    [TestFixture]
    public class LocationDetailsMapperFixture : Fixture
    {
        [Test]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<Location>();

            var mapper = new MDM.Mappers.LocationDetailsMapper();

            // Act
            var result = mapper.Map(source);

			// Assert
			Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Type, result.Type);
        }
    }
}