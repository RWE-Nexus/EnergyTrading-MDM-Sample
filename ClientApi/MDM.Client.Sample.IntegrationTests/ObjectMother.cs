namespace MDM.Client.Sample.IntegrationTests
{
    using System;

    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Sample;

    public class ObjectMother
    {
        public static T Create<T>()
            where T : IMdmEntity
        {
            var value = Create(typeof(T).Name);

            return (T)value;
        }

        public static IMdmEntity Create(string name)
        {
            switch (name)
            {
                case "Broker":
                    return CreateBroker();
                case "Counterparty":
                    return CreateCounterparty();
                case "Exchange":
                    return CreateExchange();
                case "Location":
                    return CreateLocation();
                case "Party":
                    return CreateParty();
                case "PartyRole":
                    return CreatePartyRole();
                case "Person":
                    return CreatePerson();
                case "SourceSystem":
                    return CreateSourceSystem();
                default:
                    throw new NotImplementedException("Unsupported MDM Entity type: " + name);
            }
        }

        private static IMdmEntity CreateBroker()
        {
            var guid = Guid.NewGuid();

            return new EnergyTrading.MDM.Contracts.Sample.Broker
            {
                Identifiers = CreateIdList(guid), 
                Details = new BrokerDetails
                              {
                                  Name = "Broker" + Guid.NewGuid()
                              },
            };
        }

        private static IMdmEntity CreateCounterparty()
        {
            var guid = Guid.NewGuid();

            return new EnergyTrading.MDM.Contracts.Sample.Counterparty
            {
                Identifiers = CreateIdList(guid),
                Details = new CounterpartyDetails
                              {
                                  Name = "Counterparty" + Guid.NewGuid()
                              },
            };
        }

        private static IMdmEntity CreateExchange()
        {
            var guid = Guid.NewGuid();

            return new EnergyTrading.MDM.Contracts.Sample.Exchange
            {
                Identifiers = CreateIdList(guid),
                Details = new ExchangeDetails
                              {
                                  Name = "Exchange" + Guid.NewGuid()
                              },
            };
        }

        private static IMdmEntity CreateParty()
        {
            var guid = Guid.NewGuid();

            return new EnergyTrading.MDM.Contracts.Sample.Party
            {
                Identifiers = CreateIdList(guid),
                Details = new PartyDetails
                              {
                                  Name = "Party" + Guid.NewGuid()
                              },
            };
        }

        private static IMdmEntity CreatePartyRole()
        {
            var guid = Guid.NewGuid();

            return new EnergyTrading.MDM.Contracts.Sample.PartyRole
            {
                Identifiers = CreateIdList(guid),
                Details = new PartyRoleDetails
                              {
                                  Name = "PartyRole" + Guid.NewGuid()
                              },
            };
        }

        private static IMdmEntity CreatePerson()
        {
            var guid = Guid.NewGuid();

            return new EnergyTrading.MDM.Contracts.Sample.Person
            {
                Identifiers = CreateIdList(guid),
                Details = new PersonDetails
                              {
                                  Forename = "Person" + Guid.NewGuid(),
                                  Surname = "Person" + Guid.NewGuid()
                              },
            };
        }

        private static IMdmEntity CreateSourceSystem()
        {
            var guid = Guid.NewGuid();

            return new EnergyTrading.Mdm.Contracts.SourceSystem
            {
                Identifiers = CreateIdList(guid),
                Details = new SourceSystemDetails
                              {
                                  Name = "SourceSystem" + Guid.NewGuid()
                              },
            };
        }

        private static IMdmEntity CreateLocation()
        {
            var guid = Guid.NewGuid();

            return new EnergyTrading.MDM.Contracts.Sample.Location
            {
                Identifiers = CreateIdList(guid),
                Details = new LocationDetails
                              {
                                  Name = "Location" + Guid.NewGuid()
                              },
            };
        }

        private static MdmIdList CreateIdList(Guid guid)
        {
            return new MdmIdList
                       {
                           new MdmId { SystemName = "Endur", Identifier = "Endur" + guid },
                           new MdmId { SystemName = "Trayport", Identifier = "Trayport" + guid },
                       };
        }
    }
}