namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using NUnit.Framework;

    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.MDM.Test;

    [TestFixture]
    public class BrokerDetailsMapperFixture : Fixture
    {
	    [Test]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<BrokerDetails>();

            var expected = new EnergyTrading.MDM.Contracts.Sample.BrokerDetails
            {
                Name = source.Name,
                Fax = source.Fax,
                Phone = source.Phone,
                Rate = source.Rate
            };

            var mapper = new MDM.Mappers.BrokerDetailsMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
		}
    }
}

	