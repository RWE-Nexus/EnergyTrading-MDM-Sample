namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.MDM.ServiceHost.Unity.Configuration;

    using global::MDM.ServiceHost.Unity.Sample.Configuration;

    using Microsoft.Practices.Unity;
    using NUnit.Framework;

    using Moq;

    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Validation;
    using EnergyTrading.MDM;

    using PartyRole = EnergyTrading.MDM.Contracts.Sample.PartyRole;

    [TestFixture]
    public partial class PartyRoleValidatorFixture : Fixture
    {
        [Test]
        public void ValidatorResolution()
        {
            var container = CreateContainer();
            var meConfig = new SimpleMappingEngineConfiguration(container);
            meConfig.Configure();
            container.RegisterInstance<IRepository>(new Mock<IRepository>().Object);

            var config = new PartyRoleConfiguration(container);
            config.Configure();

            var validator = container.Resolve<IValidator<PartyRole>>("partyrole");

            // Assert
            Assert.IsNotNull(validator, "Validator resolution failed");
        }

        [Test]
        public void ValidPartyRolePasses()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var system = new SourceSystem { Name = "Test" };

            var systemList = new List<SourceSystem> { system };
            var systemRepository = new Mock<IRepository>();
			var repository = new StubValidatorRepository();

            systemRepository.Setup(x => x.Queryable<MDM.SourceSystem>()).Returns(systemList.AsQueryable());

            var identifier = new EnergyTrading.Mdm.Contracts.MdmId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(-10),
                EndDate = start.AddHours(-5)
            };

            var validatorEngine = new Mock<IValidatorEngine>();
            var validator = new PartyRoleValidator(validatorEngine.Object, repository);

            var party = new PartyRole
                {
                    PartyRoleType = "SomepartyRole",
                    Details = new EnergyTrading.MDM.Contracts.Sample.PartyRoleDetails{Name = "Test"}, Identifiers = new EnergyTrading.Mdm.Contracts.MdmIdList { identifier }
                };
            this.AddRelatedEntities(party);

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(party, violations);

            // Assert
            Assert.IsTrue(result, "Validator failed");
            Assert.AreEqual(0, violations.Count, "Violation count differs");
        }

        [Test]
        public void OverlapsRangeFails()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var finish = new DateTime(2020, 12, 31);
            var validity = new DateRange(start, finish);
            var system = new SourceSystem { Name = "Test" };
            var partyMapping = new PartyRoleMapping { System = system, MappingValue = "1", Validity = validity };

            var list = new List<PartyRoleMapping> { partyMapping };
            var partyMappingRepository = new Mock<IRepository>();
            partyMappingRepository.Setup(x => x.Queryable<PartyRoleMapping>()).Returns(list.AsQueryable());

            var systemList = new List<SourceSystem>();
            var systemRepository = new Mock<IRepository>();
            systemRepository.Setup(x => x.Queryable<MDM.SourceSystem>()).Returns(systemList.AsQueryable());

            var overlapsRangeIdentifier = new EnergyTrading.Mdm.Contracts.MdmId
            {
                SystemName = "Test",
                Identifier = "1",
                StartDate = start.AddHours(10),
                EndDate = start.AddHours(15)
            };

            var identifierValidator = new NexusIdValidator<PartyRoleMapping>(partyMappingRepository.Object);
            var validatorEngine = new Mock<IValidatorEngine>();
            validatorEngine.Setup(x => x.IsValid(It.IsAny<EnergyTrading.Mdm.Contracts.MdmId>(), It.IsAny<IList<IRule>>()))
                          .Returns((EnergyTrading.Mdm.Contracts.MdmId x, IList<IRule> y) => identifierValidator.IsValid(x, y));
            var validator = new PartyRoleValidator(validatorEngine.Object, null);

            var party = new PartyRole { Identifiers = new EnergyTrading.Mdm.Contracts.MdmIdList { overlapsRangeIdentifier } };

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(party, violations);

            // Assert
            Assert.IsFalse(result, "Validator succeeded");
        }

        [Test]
        public void BadSystemFails()
        {
            // Assert
            var start = new DateTime(1999, 1, 1);
            var finish = new DateTime(2020, 12, 31);
            var validity = new DateRange(start, finish);
            var system = new SourceSystem { Name = "Test" };
            var partyMapping = new PartyRoleMapping { System = system, MappingValue = "1", Validity = validity };

            var list = new List<PartyRoleMapping> { partyMapping };
            var partyMappingRepository = new Mock<IRepository>();
            partyMappingRepository.Setup(x => x.Queryable<PartyRoleMapping>()).Returns(list.AsQueryable());

            var badSystemIdentifier = new EnergyTrading.Mdm.Contracts.MdmId
            {
                SystemName = "Jim",
                Identifier = "1",
                StartDate = start.AddHours(-10),
                EndDate = start.AddHours(-5)
            };

            var identifierValidator = new NexusIdValidator<PartyRoleMapping>(partyMappingRepository.Object);
            var validatorEngine = new Mock<IValidatorEngine>();
            validatorEngine.Setup(x => x.IsValid(It.IsAny<EnergyTrading.Mdm.Contracts.MdmId>(), It.IsAny<IList<IRule>>()))
                           .Returns((EnergyTrading.Mdm.Contracts.MdmId x, IList<IRule> y) => identifierValidator.IsValid(x, y));
            var validator = new PartyRoleValidator(validatorEngine.Object, null);

            var party = new PartyRole { Identifiers = new EnergyTrading.Mdm.Contracts.MdmIdList { badSystemIdentifier } };

            // Act
            var violations = new List<IRule>();
            var result = validator.IsValid(party, violations);

            // Assert
            Assert.IsFalse(result, "Validator succeeded");
        }    
		
		partial void AddRelatedEntities(EnergyTrading.MDM.Contracts.Sample.PartyRole contract);

    }
}

