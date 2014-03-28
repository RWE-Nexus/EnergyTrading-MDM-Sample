namespace EnergyTrading.MDM.Test.Mappers
{
    using NUnit.Framework;

    [TestFixture]
    public class PartyDetailsMapperFixture : Fixture
    {
        [Test]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<PartyDetails>();

            var expected = new EnergyTrading.MDM.Contracts.Sample.PartyDetails
            {
                Name = source.Name,
                TelephoneNumber = source.Phone,
                FaxNumber = source.Fax,
                Role = source.Role
            };

            var mapper = new MDM.Mappers.PartyDetailsMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
        }
    }
}