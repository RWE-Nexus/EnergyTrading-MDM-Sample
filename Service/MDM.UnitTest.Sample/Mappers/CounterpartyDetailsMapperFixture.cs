namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using NUnit.Framework;

    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.MDM.Test;

    [TestFixture]
    public class CounterpartyDetailsMapperFixture : Fixture
    {
	    [Test]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<CounterpartyDetails>();

	        var expected = new EnergyTrading.MDM.Contracts.Sample.CounterpartyDetails
	            {
	                Name = source.Name,
	                Fax = source.Fax,
	                Phone = source.Phone,
	                ShortName = source.ShortName,
            };

            var mapper = new MDM.Mappers.CounterpartyDetailsMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
		}
    }
}

	
