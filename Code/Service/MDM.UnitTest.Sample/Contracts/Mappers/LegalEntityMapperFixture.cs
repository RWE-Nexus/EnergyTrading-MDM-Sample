﻿namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using System;

    using Microsoft.Practices.Unity;
    using NUnit.Framework;

    using Moq;
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading;
    using EnergyTrading.Mapping;

    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Extensions;

    [TestFixture]
    public class LegalEntityMapperFixture : Fixture
    {
        [Test]
        public void Map()
        {
            // Arrange
            var start = new DateTime(2010, 1, 1);
            var end = DateUtility.MaxDate;
            var range = new DateRange(start, end);

            var id = new EnergyTrading.Mdm.Contracts.MdmId { SystemName = "Test", Identifier = "A" };
            var contractDetails = new EnergyTrading.MDM.Contracts.Sample.LegalEntityDetails();
            var contract = new EnergyTrading.MDM.Contracts.Sample.LegalEntity
            {
                Identifiers = new EnergyTrading.Mdm.Contracts.MdmIdList { id },
                Details = contractDetails,
                MdmSystemData = new EnergyTrading.Mdm.Contracts.SystemData { StartDate = start, EndDate = end },
                Party = ObjectMother.Create<Party>().CreateNexusEntityId(() => ""),
            };

            // NB Don't assign validity here, want to prove SUT sets it
            var details = new LegalEntityDetails();

            var mapping = new PartyRoleMapping();

            var mappingEngine = new Mock<IMappingEngine>();
            mappingEngine.Setup(x => x.Map<EnergyTrading.Mdm.Contracts.MdmId, PartyRoleMapping>(id)).Returns(mapping);
            mappingEngine.Setup(x => x.Map<EnergyTrading.MDM.Contracts.Sample.LegalEntityDetails, LegalEntityDetails>(contractDetails)).Returns(details);
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.FindOne<Party>(int.Parse(contract.Party.Identifier.Identifier))).Returns(ObjectMother.Create<Party>());

            var mapper = new LegalEntityMapper(repository.Object, mappingEngine.Object);

            // Act
            var candidate = mapper.Map(contract);

            // Assert
            //Assert.AreEqual(1, candidate.Details.Count, "Detail count differs");
            Assert.AreEqual(1, candidate.Mappings.Count, "Mapping count differs");
            Check(range, details.Validity, "Validity differs");
        }
    }
}
