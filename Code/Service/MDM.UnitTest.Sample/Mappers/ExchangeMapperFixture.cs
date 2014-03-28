using NUnit.Framework;
using EnergyTrading.MDM.Mappers;

namespace EnergyTrading.MDM.Test.Mappers
{
    [TestFixture]
    public class ExchangeMapperFixture
    {
        [Test]
        public void Map_NoConditions_MapsPartyIdToPartyName()
        {
            //Arrange 
            var source = new MDM.Exchange() { Party = new Party() { Id = 999 } };
            source.Party.AddDetails(new PartyDetails(){ Name = "999" });
            var mapper = new ExchangeMapper();

            //Act
            var destination = mapper.Map(source);

            //Assert
            Assert.AreEqual("999", destination.Party.Name);
        }
    }
}
