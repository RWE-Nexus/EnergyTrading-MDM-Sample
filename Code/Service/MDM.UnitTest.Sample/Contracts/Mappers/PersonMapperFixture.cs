namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using System;

    using EnergyTrading.MDM.ServiceHost.Unity.Configuration;

    using global::MDM.ServiceHost.Unity.Sample.Configuration;

    using Microsoft.Practices.Unity;
    using NUnit.Framework;

    using Moq;

    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading;
    using EnergyTrading.Mapping;

    using EnergyTrading.MDM;

    [TestFixture]
    public class PersonMapperFixture : Fixture
    {
        [Test]
        public void Resolve()
        {
            var container = CreateContainer();
            var meConfig = new SimpleMappingEngineConfiguration(container);
            meConfig.Configure();

            var config = new PersonConfiguration(container);
            config.Configure();

            var validator = container.Resolve<IMapper<EnergyTrading.MDM.Contracts.Sample.Person, Person>>();

            // Assert
            Assert.IsNotNull(validator, "Mapper resolution failed");
        }

        [Test]
        public void Map()
        {
            // Arrange
            var start = new DateTime(2010, 1, 1);
            var end = DateUtility.MaxDate;
            var range = new DateRange(start, end);

            var id = new EnergyTrading.Mdm.Contracts.MdmId { SystemName = "Test", Identifier = "A" };
            var contractDetails = new EnergyTrading.MDM.Contracts.Sample.PersonDetails();
            var contract = new EnergyTrading.MDM.Contracts.Sample.Person
                {
                    Identifiers = new EnergyTrading.Mdm.Contracts.MdmIdList { id },
                    Details = contractDetails,
                    MdmSystemData = new EnergyTrading.Mdm.Contracts.SystemData { StartDate = start, EndDate = end }
                };

            // NB Don't assign validity here, want to prove SUT sets it
            var personDetails = new PersonDetails();

            var mapping = new PersonMapping();

            var mappingEngine = new Mock<IMappingEngine>();
            mappingEngine.Setup(x => x.Map<EnergyTrading.Mdm.Contracts.MdmId, PersonMapping>(id)).Returns(mapping);
            mappingEngine.Setup(x => x.Map<EnergyTrading.MDM.Contracts.Sample.PersonDetails, PersonDetails>(contractDetails)).Returns(personDetails);

            var mapper = new PersonMapper(mappingEngine.Object);

            // Act
            var candidate = mapper.Map(contract);

            // Assert
            Assert.AreEqual(1, candidate.Details.Count, "Detail count differs");
            Assert.AreEqual(1, candidate.Mappings.Count, "Mapping count differs");
            Check(range, personDetails.Validity, "Validity differs");
        }
    }
}