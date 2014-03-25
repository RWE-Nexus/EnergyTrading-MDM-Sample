namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using NUnit.Framework;

    using Moq;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Data;

    [TestFixture]
    public class LocationDetailsMapperFixture : Fixture
    {
        [Test]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();

            var source = new EnergyTrading.MDM.Contracts.Sample.LocationDetails
                {
                   Name = "Test Name",
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.LocationDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, source.Name);
        }
    }
}
		