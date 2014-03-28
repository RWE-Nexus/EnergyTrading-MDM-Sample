namespace EnergyTrading.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using Moq;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;
    using EnergyTrading.Search;

    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;

    using DateRange = EnergyTrading.DateRange;
	using SourceSystem = EnergyTrading.MDM.SourceSystem;

    [TestFixture]
    public class LocationUpdateMappingFixture
    {
        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void NullContractInvalid()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            validatorFactory.Setup(x => x.IsValid(It.IsAny<object>(), It.IsAny<IList<IRule>>())).Returns(false);

            var service = new LocationService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Act
            service.UpdateMapping(null);
        }

        [Test]
        public void EntityNotFound()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new LocationService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var message = new AmendMappingRequest
            {
                EntityId = 12,
                Mapping = new MdmId { SystemName = "Test", Identifier = "A" }
            };

            validatorFactory.Setup(x => x.IsValid(It.IsAny<AmendMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);

            // Act
            var candidate = service.UpdateMapping(message);

            // Assert
            Assert.IsNull(candidate);
        }

        [Test]
        [ExpectedException(typeof(VersionConflictException))]
        public void VersionConflict()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new LocationService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var message = new AmendMappingRequest
            {
                MappingId = 12,
                Mapping = new MdmId { SystemName = "Test", Identifier = "A" },
                Version = 34
            };

            var mapping = new LocationMapping { Location = new MDM.Location() { Timestamp = BitConverter.GetBytes(25L)} };

            validatorFactory.Setup(x => x.IsValid(It.IsAny<AmendMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            repository.Setup(x => x.FindOne<LocationMapping>(12)).Returns(mapping);

            // Act
            service.UpdateMapping(message);
        }

        [Test]
        public void ValidDetailsSaved()
        {
            const int mappingId = 12;
		
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new LocationService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // NB Don't put mappingId here - service assigns it
            var identifier = new MdmId { SystemName = "Test", Identifier = "A" };
            var message = new AmendMappingRequest { MappingId = mappingId, Mapping = identifier, Version = 34 };

            var start = new DateTime(2000, 12, 31);
            var finish = DateUtility.Round(SystemTime.UtcNow().AddDays(5));
            var s1 = new SourceSystem { Name = "Test" };
            var m1 = new LocationMapping { Id = mappingId, System = s1, MappingValue = "1", Version = 34UL.GetVersionByteArray(), Validity = new DateRange(start, DateUtility.MaxDate) };			
            var m2 = new LocationMapping { Id = mappingId, System = s1, MappingValue = "1", Validity = new DateRange(start, finish) };

            // NB We deliberately bypasses the business logic
            var entity = new MDM.Location();
            m1.Location = entity;
            entity.Mappings.Add(m1);

            validatorFactory.Setup(x => x.IsValid(It.IsAny<AmendMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            repository.Setup(x => x.FindOne<LocationMapping>(mappingId)).Returns(m1);
            mappingEngine.Setup(x => x.Map<EnergyTrading.Mdm.Contracts.MdmId, LocationMapping>(identifier)).Returns(m2);

            // Act
            service.UpdateMapping(message);

            // Assert
            Assert.AreEqual(mappingId, identifier.MappingId, "Mapping identifier differs");
			// Proves we have an update not an add
			Assert.AreEqual(1, entity.Mappings.Count, "Mapping count differs"); 
            // NB Don't verify result of Update - already covered by LocationMappingFixture
            repository.Verify(x => x.Save(entity));
            repository.Verify(x => x.Flush());
        }
    }
}
	