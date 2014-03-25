namespace EnergyTrading.MDM.Test.Mappers
{
    using NUnit.Framework;

    [TestFixture]
    public class PartyRoleDetailsMapperFixture : Fixture
    {
        [Test]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<PartyRoleDetails>();

            var expected = new EnergyTrading.MDM.Contracts.Sample.PartyRoleDetails
            {
                Name = source.Name,
            };

            var mapper = new MDM.Mappers.PartyRoleDetailsMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
        }
    }
}
