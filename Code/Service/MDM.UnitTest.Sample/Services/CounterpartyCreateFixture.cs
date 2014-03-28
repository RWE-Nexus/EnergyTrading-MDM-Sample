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
    public class CounterpartyCreateFixture
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

            var service = new CounterpartyService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

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

            var service = new CounterpartyService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var contract = new EnergyTrading.MDM.Contracts.Sample.Counterparty();

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

            var service = new CounterpartyService(validatorFactory.Object, mappingEngine.Object, repository.Object, searchCache.Object);

            var counterparty = new Counterparty();
            var contract = new EnergyTrading.MDM.Contracts.Sample.Counterparty();

            validatorFactory.Setup(x => x.IsValid(It.IsAny<EnergyTrading.MDM.Contracts.Sample.Counterparty>(), It.IsAny<IList<IRule>>())).Returns(true);
            mappingEngine.Setup(x => x.Map<EnergyTrading.MDM.Contracts.Sample.Counterparty, Counterparty>(contract)).Returns(counterparty);

            // Act
            var expected = service.Create(contract);

            // Assert
            Assert.AreSame(expected, counterparty, "Counterparty differs");
            repository.Verify(x => x.Add(counterparty));
            repository.Verify(x => x.Flush());
        }
    }
}
	