namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using NUnit.Framework;

    [TestFixture]
    public class PartyDetailsMapperFixture : Fixture
    {
        [Test]
        public void Map()
        {
            // Arrange
            var source = new EnergyTrading.MDM.Contracts.Sample.PartyDetails
                {
                    Name = "John",
                    TelephoneNumber = "020 7745 1232",
                    FaxNumber = "020 1232 1232",
                    Role = "Trader",
                    IsInternal = true
                };

            var expected = new MDM.PartyDetails()
                {
                    Name = "John",
                    Phone = "020 7745 1232",
                    Fax = "020 1232 1232",
                    Role = "Trader",
                    IsInternal = true
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.PartyDetailsMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
        }
    }
}
