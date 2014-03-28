namespace EnergyTrading.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using Moq;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Services;
    using EnergyTrading.Search;
    using EnergyTrading.Test;
    using EnergyTrading.Validation;
    using DateRange = EnergyTrading.DateRange;
    using Party = EnergyTrading.MDM.Party;
    using PartyDetails = EnergyTrading.MDM.PartyDetails;

    // TOOD:SEARCH - Add this to the templates for each entity
    [TestFixture]
    public class when_the_service_is_asked_for_search_results :
        SpecBase<MdmService<EnergyTrading.MDM.Contracts.Sample.Party, Party, PartyMapping, PartyDetails, EnergyTrading.MDM.Contracts.Sample.PartyDetails>>
    {
        protected Mock<ISearchCache> cacheStub;

        protected Mock<IRepository> repositoryStub;

        protected Guid result;

        private CacheSearchResultPage cacheResults;
        private Mock<IMappingEngine> mappingEngineStub;

        private int pageNumber;
        private SearchResultPage<EnergyTrading.MDM.Contracts.Sample.Party> results;
        private string resultsKey;
        private Mock<IValidatorEngine> validatorStub;

        [Test]
        public void should_ask_the_cache_for_the_results()
        {
            this.cacheStub.Verify(cache => cache.Get(this.resultsKey, this.pageNumber));
        }

        [Test]
        public void should_ask_the_repository_to_get_the_entities_in_the_search()
        {
            this.repositoryStub.Verify(x => x.Queryable<Party>()); 
        }

        [Test]
        public void should_return_one_result()
        {
            Assert.AreEqual(1, this.results.Contracts.Count);
        }

        protected override void Because_of()
        {
            this.results = this.Sut.GetSearchResults(this.resultsKey, this.pageNumber);
        }

        protected override
            MdmService<EnergyTrading.MDM.Contracts.Sample.Party, Party, PartyMapping, PartyDetails, EnergyTrading.MDM.Contracts.Sample.PartyDetails> Establish_context()
        {
            this.validatorStub = new Mock<IValidatorEngine>();
            this.mappingEngineStub = new Mock<IMappingEngine>();
            this.repositoryStub = new Mock<IRepository>();
            this.cacheStub = new Mock<ISearchCache>();

            this.resultsKey = Guid.NewGuid().ToString();
            this.pageNumber = 1;
            this.cacheResults = new CacheSearchResultPage(new List<int> { 1 }, DateTime.Now, null, this.resultsKey);
            this.cacheStub.Setup(cache => cache.Get(this.resultsKey, this.pageNumber)).Returns(this.cacheResults);

            var party = new Party() { Id = 1, };

            party.AddDetails(new PartyDetails() { Validity = new EnergyTrading.DateRange() });

            var entities = new List<Party> { party }.AsQueryable();

            this.repositoryStub.Setup(x => x.Queryable<Party>()).Returns(entities);
            return new PartyService(
                this.validatorStub.Object,
                this.mappingEngineStub.Object,
                this.repositoryStub.Object,
                this.cacheStub.Object);
        }
    }

    [TestFixture]
    public class when_the_service_is_asked_for_search_results_and_the_results_are_not_found :
        SpecBase<MdmService<EnergyTrading.MDM.Contracts.Sample.Party, Party, PartyMapping, PartyDetails, EnergyTrading.MDM.Contracts.Sample.PartyDetails>>
    {
        protected Mock<ISearchCache> cacheStub;

        protected Mock<IRepository> repositoryStub;

        protected Guid result;

        private Mock<IMappingEngine> mappingEngineStub;

        private int pageNumber;

        private string resultsKey;

        private Mock<IValidatorEngine> validatorStub;

        private SearchResultPage<EnergyTrading.MDM.Contracts.Sample.Party> results;

        [Test]
        public void should_ask_the_cache_for_the_results()
        {
            this.cacheStub.Verify(cache => cache.Get(this.resultsKey, this.pageNumber));
        }

        [Test]
        public void should_not_ask_the_repository_to_get_the_entities_in_the_search()
        {
            this.repositoryStub.Verify(x => x.Queryable<Party>(), Times.Never()); 
        }

        [Test]
        public void should_return_null()
        {
            Assert.AreEqual(null, this.results);
        }

        protected override void Because_of()
        {
            this.results = this.Sut.GetSearchResults(this.resultsKey, this.pageNumber);
        }

        protected override
            MdmService<EnergyTrading.MDM.Contracts.Sample.Party, Party, PartyMapping, PartyDetails, EnergyTrading.MDM.Contracts.Sample.PartyDetails> Establish_context()
        {
            this.validatorStub = new Mock<IValidatorEngine>();
            this.mappingEngineStub = new Mock<IMappingEngine>();
            this.repositoryStub = new Mock<IRepository>();
            this.cacheStub = new Mock<ISearchCache>();

            this.resultsKey = Guid.NewGuid().ToString();
            this.pageNumber = 1;

            return new PartyService(
                this.validatorStub.Object,
                this.mappingEngineStub.Object,
                this.repositoryStub.Object,
                this.cacheStub.Object);
        }
    }
}