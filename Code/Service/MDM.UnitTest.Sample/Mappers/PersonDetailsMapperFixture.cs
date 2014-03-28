﻿namespace EnergyTrading.MDM.Test.Mappers
{
    using NUnit.Framework;

    [TestFixture]
    public class PersonDetailsMapperFixture : Fixture
    {
        [Test]
        public void Map()
        {
            // Arrange
            var source = new MDM.PersonDetails()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Phone = "020 7745 1232",
                    Fax = "020 1232 1232",
                    Role = "Trader",
                    Email = "test@test.com"
                };

            var expected = new EnergyTrading.MDM.Contracts.Sample.PersonDetails
                {
                    Forename = "John",
                    Surname = "Smith",
                    TelephoneNumber = "020 7745 1232",
                    FaxNumber = "020 1232 1232",
                    Role = "Trader",
                    Email = "test@test.com"
                };

            var mapper = new MDM.Mappers.PersonDetailsMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
        }
    }
}
