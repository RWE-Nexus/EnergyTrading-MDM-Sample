namespace EnergyTrading.MDM.Test.Services
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using Moq;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;
    using EnergyTrading.Search;
    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;

    [TestFixture]
    public class PartyRoleCreateMappingFixture : Fixture
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

            var service = new PartyRoleService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            validatorFactory.Setup(x => x.IsValid(It.IsAny<object>(), It.IsAny<IList<IRule>>())).Returns(false);

            // Act
            service.CreateMapping(null);
        }

        [Test]
        public void EntityNotFound()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new PartyRoleService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var message = new CreateMappingRequest
                {
                    EntityId = 12,
                    Mapping = new MdmId { SystemName = "Test", Identifier = "A" }
                };

            validatorFactory.Setup(x => x.IsValid(It.IsAny<CreateMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);

            // Act
            var candidate = service.CreateMapping(message);

            // Assert
            Assert.IsNull(candidate);
        }

        [Test]
        public void ValidContractAdded()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new PartyRoleService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);
            var identifier = new MdmId { SystemName = "Test", Identifier = "A" };
            var message = new CreateMappingRequest
            {
                EntityId = 12,
                Mapping = identifier
            };

            var system = new MDM.SourceSystem { Name = "Test" };
            var mapping = new PartyRoleMapping { System = system, MappingValue = "A" };
            validatorFactory.Setup(x => x.IsValid(It.IsAny<CreateMappingRequest>(), It.IsAny<IList<IRule>>())).Returns(true);
            mappingEngine.Setup(x => x.Map<EnergyTrading.Mdm.Contracts.MdmId, PartyRoleMapping>(identifier)).Returns(mapping);

            var partyrole = new MDM.PartyRole();
            repository.Setup(x => x.FindOne<MDM.PartyRole>(12)).Returns(partyrole);

            // Act
            var candidate = (PartyRoleMapping)service.CreateMapping(message);

            // Assert
            Assert.AreSame(mapping, candidate);
            repository.Verify(x => x.Save(partyrole));
            repository.Verify(x => x.Flush());
        }
    }
}
	