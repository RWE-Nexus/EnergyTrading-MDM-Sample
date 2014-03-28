﻿namespace EnergyTrading.MDM.Test.Mappers
{
    using System;

    using NUnit.Framework;

    using EnergyTrading;
    using EnergyTrading.MDM.Mappers;
    using EnergyTrading.MDM;

    [TestFixture]
    public class PersonMappingMapperFixture : Fixture
    {
        [Test]
        public void Map()
        {
            // Arrange
            var system = new SourceSystem { Name = "Test" };
            var start = new DateTime(2010, 1, 1);
            var end = new DateTime(2012, 12, 31);

            var source = new PersonMapping
                {
                    Id = 100,
                    System = system,
                    MappingValue = "1",
                    IsMaster = true,
                    Validity = new DateRange(start, end)
                };
            var expected = new EnergyTrading.Mdm.Contracts.MdmId
                {
                    MappingId = 100,
                    SystemName = "Test",
                    Identifier = "1",
                    SourceSystemOriginated = true,
                    StartDate = start,
                    EndDate = end
                };

            var mapper = new PersonMappingMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
        }
    }
}