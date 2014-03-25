namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading.Data;

    public static class ObjectMother
    {
        public static T Create<T>()
            where T : IIdentifiable
        {
            var value = Create(typeof(T).Name);

            return (T)value;
        }

        public static IIdentifiable Create(string name)
        {
            switch (name)
            {
                case "Broker":
                    var broker = new Broker { PartyRoleType = "Broker" };
                    var brokerDetails = Create<BrokerDetails>();
                    broker.Party = Create<Party>();
                    brokerDetails.PartyRole = broker;
                    broker.Details.Add(brokerDetails);
                    return broker;

                case "BrokerDetails":
                    return new BrokerDetails
                    {
                        Name = "Name" + Guid.NewGuid(),
                        Fax = "01302111111",
                        Phone = "01302222222",
                        Rate = 1.1m
                    };

                case "Counterparty":
                    var counterparty = new Counterparty { PartyRoleType = "Counterparty" };
                    var counterpartyDetails = Create<CounterpartyDetails>();
                    counterparty.Party = Create<Party>();
                    counterpartyDetails.PartyRole = counterparty;
                    counterparty.Details.Add(counterpartyDetails);
                    return counterparty;

                case "CounterpartyDetails":
                    return new CounterpartyDetails
                    {
                        Name = "Name" + Guid.NewGuid(),
                        Fax = "01302111111",
                        Phone = "01302222222",
                        ShortName = "sh",
                        //TaxLocation = Create<Location>()
                    };

                case "Exchange":
                    var exchange = new Exchange { PartyRoleType = "Exchange", Party = Create<Party>() };
                    var exchangeDetails = Create<ExchangeDetails>();
                    exchangeDetails.PartyRole = exchange;
                    exchange.Details.Add(exchangeDetails);
                    return exchange;

                case "ExchangeDetails":
                    return
                    new ExchangeDetails
                    {
                        Name = "Name" + Guid.NewGuid(),
                        Phone = "118118"
                    };


                case "LegalEntity":
                    var legalEntity = new LegalEntity { PartyRoleType = "LegalEntity" };
                    var legalEntityDetails = Create<LegalEntityDetails>();
                    legalEntity.Party = Create<Party>();
                    legalEntityDetails.PartyRole = legalEntity;
                    legalEntity.Details.Add(legalEntityDetails);
                    return legalEntity;

                case "LegalEntityDetails":
                    return new LegalEntityDetails
                    {
                        Name = "Name" + Guid.NewGuid(),
                        RegisteredName = "RegisteredName",
                        RegistrationNumber = "RegistrationNumber",
                        Address = "123 Fake St",
                        Website = "http://test.com",
                        CountryOfIncorporation = "Germany",
                        Email = "foo@bar.com",
                        Fax = "020 1234 5678",
                        Phone = "020 3469 1256",
                        PartyStatus = "Active",
                        CustomerAddress = "456 Wrong Road",
                        InvoiceSetup = "Customer",
                        VendorAddress = "789 Right Road"
                    };

                case "Location":
                    return new Location
                    {
                        Type = "LocationType" + Guid.NewGuid(),
                        Name = "Location" + Guid.NewGuid(),
                    };

                case "Party":
                    var partyDetails = Create<PartyDetails>();
                    var party = new Party();
                    party.Details.Add(partyDetails);
                    partyDetails.Party = party;
                    return party;

                case "PartyDetails":
                    var ptDetails = new PartyDetails
                    {
                        Name = "Name" + Guid.NewGuid(),
                        Phone = "020 8823 1234",
                        Fax = "020 834 1237",
                        Role = "Trader"
                    };
                    return ptDetails;

                case "PartyRole":
                    var partyRole = new PartyRole { PartyRoleType = "SomeRole", Party = Create<Party>() };
                    partyRole.Details.Add(Create<PartyRoleDetails>());
                    return partyRole;

                case "PartyRoleDetails":
                    return
                    new PartyRoleDetails
                    {
                        Name = "Name" + Guid.NewGuid(),
                    };

                case "Person":
                    var personDetails = Create<PersonDetails>();
                    var person = new Person();
                    person.Details.Add(personDetails);
                    return person;

                case "PersonDetails":
                    var psDetails = new PersonDetails
                    {
                        FirstName = "Firstname" + Guid.NewGuid(),
                        LastName = "Lastname" + Guid.NewGuid(),
                        Email = "test@test.com"
                    };
                    return psDetails;

                case "SourceSystem":
                    return new SourceSystem
                    {
                        Name = "SourceSystem" + Guid.NewGuid()
                    };

                default:
                    throw new NotImplementedException("No OM for " + name);
            }
        }
    }
}