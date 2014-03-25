namespace EnergyTrading.MDM.Test.Extensions
{
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Extensions;
    using NUnit.Framework;
    using EnergyTrading.MDM.Messages;

    [TestFixture]
    public class MappingRequestExtensionsFixture
    {
        [Test]
        public void IsNexusMappingRequest_EmptySourceSystem_ReturnsFalse()
        {
            var mappingRequest = new MappingRequest();
            Assert.IsFalse(mappingRequest.IsNexusMappingRequest());
        }

        [Test]
        public void IsNexusMappingRequest_NexusSourceSystem_ReturnsTrue()
        {
            var mappingRequest = new MappingRequest { SystemName = SourceSystemNames.Nexus, Identifier = "999" };
            Assert.IsTrue(mappingRequest.IsNexusMappingRequest());
        }

        [Test]
        public void HasNumericIdentifier_WithInteger_ReturnsTrue()
        {
            var mappingRequest = new MappingRequest { SystemName = SourceSystemNames.Nexus, Identifier = "999" };
            Assert.IsTrue(mappingRequest.HasNumericIdentifier());
        }

        [Test]
        public void HasNumericIdentifier_WithNaN_ReturnsTrue()
        {
            var mappingRequest = new MappingRequest { SystemName = SourceSystemNames.Nexus, Identifier = "string" };
            Assert.IsFalse(mappingRequest.HasNumericIdentifier());
        }
    }
}
