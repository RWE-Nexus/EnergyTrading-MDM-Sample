namespace EnergyTrading.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using Moq;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Test;
    using EnergyTrading.Validation;
    using EnergyTrading.MDM.Services;

    using DateRange = EnergyTrading.DateRange;
    using Party = EnergyTrading.MDM.Party;
    using PartyDetails = EnergyTrading.MDM.PartyDetails;

    //TOOD:SEARCH - Add this to the templates for each entity
    // [TestFixture]
    public class when_the_service_is_asked_to_execute_a_search_and_no_entities_are_found : create_search_context
    {
        // [Test]
        public void should_return_a_not_found_status_code()
        {
            Assert.AreEqual(Guid.Empty.ToString(), result);
        }
    }

    [TestFixture]
    public class when_the_service_is_asked_to_execute_and_some_entities_are_found : create_search_context
    {
        protected override MdmService<EnergyTrading.MDM.Contracts.Sample.Party, Party, PartyMapping, PartyDetails, EnergyTrading.MDM.Contracts.Sample.PartyDetails> Establish_context()
        {
            var context = base.Establish_context();
            var party = new Party() { Id = 1};
            var partyDetails = new PartyDetails() { Validity = DateRange.MaxDateRange, Fax = "Test" };
            party.AddDetails(partyDetails);

            this.repositoryStub.Setup(repository => repository.Queryable<PartyDetails>()).Returns(
                (new List<PartyDetails> { partyDetails }).AsQueryable);

            this.repositoryStub.Setup(repository => repository.Queryable<Party>()).Returns(
                (new List<Party> { party }).AsQueryable);
            return context;
        }

        [Test]
        public void should_return_an_identifier_for_the_search()
        {
            Assert.AreNotEqual(Guid.Empty.ToString(), result);
        }

        [Test]
        public void should_add_the_contracts_to_the_cache()
        {
            cacheStub.Verify(cache => cache.Add(It.IsAny<string>(), It.IsAny<SearchResult>()));
        }
    }

    public class create_search_context : SpecBase<MdmService<EnergyTrading.MDM.Contracts.Sample.Party, Party, PartyMapping, PartyDetails, EnergyTrading.MDM.Contracts.Sample.PartyDetails>>
    {
        private Mock<IValidatorEngine> validatorStub;
        private Mock<IMappingEngine> mappingEngineStub;
        protected Mock<ISearchCache> cacheStub;
        protected Mock<IRepository> repositoryStub;

        protected string result;

        protected override MdmService<EnergyTrading.MDM.Contracts.Sample.Party, Party, PartyMapping, PartyDetails, EnergyTrading.MDM.Contracts.Sample.PartyDetails> Establish_context()
        {
            validatorStub = new Mock<IValidatorEngine>();
            mappingEngineStub = new Mock<IMappingEngine>();
            repositoryStub = new Mock<IRepository>();
            cacheStub = new Mock<ISearchCache>();

            return new PartyService(validatorStub.Object, mappingEngineStub.Object, repositoryStub.Object, cacheStub.Object);
        }

        protected override void Because_of()
        {
            result = Sut.CreateSearch(new Search() { SearchFields = new SearchFields() { Criterias = new List<SearchCriteria>() } });
        }
    }
}