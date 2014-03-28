namespace EnergyTrading.MDM.Test.Services
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using Moq;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;
    using EnergyTrading.MDM.Services;

    [TestFixture]
    public class PartyRoleCreateFixture
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
            service.Create(null);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void InvalidContractNotSaved()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new PartyRoleService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var contract = new EnergyTrading.MDM.Contracts.Sample.PartyRole();

            validatorFactory.Setup(x => x.IsValid(It.IsAny<object>(), It.IsAny<IList<IRule>>())).Returns(false);

            // Act
            service.Create(contract);
        }

        [Test]
        public void ValidContractIsSaved()
        {
            // Arrange
            var validatorFactory = new Mock<IValidatorEngine>();
            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
			var searchCache = new Mock<ISearchCache>();

            var service = new PartyRoleService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var partyrole = new PartyRole();
            var contract = new EnergyTrading.MDM.Contracts.Sample.PartyRole();

            validatorFactory.Setup(x => x.IsValid(It.IsAny<EnergyTrading.MDM.Contracts.Sample.PartyRole>(), It.IsAny<IList<IRule>>())).Returns(true);
            mappingEngine.Setup(x => x.Map<EnergyTrading.MDM.Contracts.Sample.PartyRole, PartyRole>(contract)).Returns(partyrole);

            // Act
            var expected = service.Create(contract);

            // Assert
            Assert.AreSame(expected, partyrole, "PartyRole differs");
            repository.Verify(x => x.Add(partyrole));
            repository.Verify(x => x.Flush());
        }
    }
}
	