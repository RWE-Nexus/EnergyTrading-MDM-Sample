using System;
using NUnit.Framework;
using Moq;
using EnergyTrading.Data;
using EnergyTrading.Mapping;
using EnergyTrading.MDM.Contracts.Mappers;
using EnergyTrading.MDM.Extensions;

namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using EnergyTrading;

    [TestFixture]
    public class BrokerMapperFixture : Fixture
    {
        [Test]
        public void Map()
        {
            // Arrange
            var start = new DateTime(2010, 1, 1);
            var end = DateUtility.MaxDate;
            var range = new DateRange(start, end);

            var id = new EnergyTrading.Mdm.Contracts.MdmId { SystemName = "Test", Identifier = "A" };
            var contractDetails = new EnergyTrading.MDM.Contracts.Sample.BrokerDetails();
            var contract = new EnergyTrading.MDM.Contracts.Sample.Broker
            {
                Identifiers = new EnergyTrading.Mdm.Contracts.MdmIdList { id },
                Details = contractDetails,
                MdmSystemData = new EnergyTrading.Mdm.Contracts.SystemData { StartDate = start, EndDate = end },
                Party = ObjectMother.Create<Party>().CreateNexusEntityId(() => "")
            };

            // NB Don't assign validity here, want to prove SUT sets it
            var details = new BrokerDetails();

            var mapping = new PartyRoleMapping();

            var mappingEngine = new Mock<IMappingEngine>();
            var repository = new Mock<IRepository>();
            mappingEngine.Setup(x => x.Map<EnergyTrading.Mdm.Contracts.MdmId, PartyRoleMapping>(id)).Returns(mapping);
            mappingEngine.Setup(x => x.Map<EnergyTrading.MDM.Contracts.Sample.BrokerDetails, BrokerDetails>(contractDetails)).Returns(details);
            repository.Setup(x => x.FindOne<Party>(int.Parse(contract.Party.Identifier.Identifier))).Returns(ObjectMother.Create<Party>());

            var mapper = new BrokerMapper(mappingEngine.Object, repository.Object);

            // Act
            var candidate = mapper.Map(contract);

            // Assert
            //Assert.AreEqual(1, candidate.Details.Count, "Detail count differs");
            Assert.AreEqual(1, candidate.Mappings.Count, "Mapping count differs");
            Assert.AreEqual("Broker", candidate.PartyRoleType);
            Check(range, details.Validity, "Validity differs");
        }
    }
}