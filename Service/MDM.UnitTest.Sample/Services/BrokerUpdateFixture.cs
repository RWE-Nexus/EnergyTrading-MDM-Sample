namespace EnergyTrading.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using Moq;

    using EnergyTrading.MDM;
    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;

    [TestFixture]
    public class BrokerUpdateFixture
    {
        [Test]
        [ExpectedException(typeof(VersionConflictException))]
        public void EarlierVersionRaisesVersionConflict()
        {
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            validatorFactory.Setup(x => x.IsValid(It.IsAny<EnergyTrading.MDM.Contracts.Sample.Broker>(), It.IsAny<IList<IRule>>())).Returns(true);

            var service = new BrokerService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var cd = new EnergyTrading.MDM.Contracts.Sample.BrokerDetails { Name = "Test" };
            var nexus = new EnergyTrading.Mdm.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var contract = new EnergyTrading.MDM.Contracts.Sample.Broker { Details = cd, MdmSystemData = nexus };

            var details = new BrokerDetails { Id = 2, Name = "Test" };
            var entity = new Broker();
            entity.AddDetails(details);

            repository.Setup(x => x.FindOne<Broker>(1)).Returns(entity);

            // Act
            service.Update(1, 1, contract);
        }

        [Test]
        public void ValidDetailsSaved()
        {
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            // Contract
            var cd = new EnergyTrading.MDM.Contracts.Sample.BrokerDetails { Name = "Test" };
            var nexus = new EnergyTrading.Mdm.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var identifier = new EnergyTrading.Mdm.Contracts.MdmId { SystemName = "Test", Identifier = "A" };
            var contract = new EnergyTrading.MDM.Contracts.Sample.Broker { Details = cd, MdmSystemData = nexus };
            contract.Identifiers.Add(identifier);

            // Domain
            var system = new SourceSystem { Name = "Test" };
            var mapping = new PartyRoleMapping { System = system, MappingValue = "A" };
            var d1 = new BrokerDetails { Id = 1, Name = "Test", Timestamp = 74UL.GetVersionByteArray() };
            var entity = new Broker() { Party = new Party() { Id = 999 } };
            entity.AddDetails(d1);

            var d2 = new BrokerDetails { Name = "Test" };
            var range = new DateRange(new DateTime(2012, 1, 1), DateTime.MaxValue);

            validatorFactory.Setup(x => x.IsValid(It.IsAny<CreateMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            validatorFactory.Setup(x => x.IsValid(It.IsAny<EnergyTrading.MDM.Contracts.Sample.Broker>(), It.IsAny<IList<IRule>>())).Returns(true);

            repository.Setup(x => x.FindOne<Broker>(1)).Returns(entity);

            mappingEngine.Setup(x => x.Map<EnergyTrading.MDM.Contracts.Sample.BrokerDetails, BrokerDetails>(cd)).Returns(d2);
            mappingEngine.Setup(x => x.Map<EnergyTrading.Mdm.Contracts.SystemData, DateRange>(nexus)).Returns(range);
            mappingEngine.Setup(x => x.Map<EnergyTrading.Mdm.Contracts.MdmId, PartyRoleMapping>(identifier)).Returns(mapping);

            var service = new BrokerService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            // Act
            service.Update(1, 74, contract);

            // Assert
            Assert.AreEqual(2, entity.Details.Count, "Details count differs");
            Assert.AreEqual(1, entity.Mappings.Count, "Mapping count differs");
            repository.Verify(x => x.Save(entity));
            repository.Verify(x => x.Flush());
        }

        [Test]
        public void EntityNotFound()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            var searchCache = new Mock<ISearchCache>();

            var service = new BrokerService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var cd = new EnergyTrading.MDM.Contracts.Sample.BrokerDetails { Name = "Test" };
            var nexus = new EnergyTrading.Mdm.Contracts.SystemData { StartDate = new DateTime(2012, 1, 1) };
            var contract = new EnergyTrading.MDM.Contracts.Sample.Broker { Details = cd, MdmSystemData = nexus };

            validatorFactory.Setup(x => x.IsValid(It.IsAny<EnergyTrading.MDM.Contracts.Sample.Broker>(), It.IsAny<IList<IRule>>())).Returns(true);

            // Act
            var response = service.Update(1, 1, contract);

            // Assert
            Assert.IsNotNull(response, "Response is null");
            Assert.IsFalse(response.IsValid, "Response is valid");
            Assert.AreEqual(ErrorType.NotFound, response.Error.Type, "ErrorType differs");
        }
    }
}
	