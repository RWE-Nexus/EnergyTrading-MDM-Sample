namespace EnergyTrading.MDM.Test.Mappers
{
    using NUnit.Framework;

    using EnergyTrading.MDM.Extensions;

    [TestFixture]
    public class ExchangeDetailsMapperFixture : Fixture
    {
        [Test]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<ExchangeDetails>();

            var expected = new EnergyTrading.MDM.Contracts.Sample.ExchangeDetails
            {
                Name = source.Name,
                Fax = source.Fax,
                Phone = source.Phone,
            };

            var mapper = new MDM.Mappers.ExchangeDetailsMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
        }
    }
}

