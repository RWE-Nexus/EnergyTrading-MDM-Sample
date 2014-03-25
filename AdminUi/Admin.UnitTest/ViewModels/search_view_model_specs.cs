namespace Admin.UnitTest.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Admin.UnitTest.Framework;

    using Common.EventAggregator;
    using Common.Events;
    using Common.Extensions;
    using Common.Services;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Contracts.Search;

    using Shell.ViewModels;

    [TestClass]
    public class when_performing_an_empty_name_search : SearchViewModelTestBase
    {
        protected override void Because_of()
        {
            this.Sut.IsMappingSearch = false;
            this.Sut.NameSearch = null;
            this.Sut.Search();
        }

        [TestMethod] 
        public void search_should_not_be_null()
        {
            Assert.IsNotNull(this.search);
        }

        [TestMethod]
        public void search_should_contain_no_criteria()
        {
            Assert.AreEqual(0, this.search.SearchFields.Criterias.Count);
        }
    }

    [TestClass]
    public class when_performing_a_name_search_with_value : SearchViewModelTestBase
    {
        protected override void Establish_context()
        {
            base.Establish_context();
            this.Sut.SelectedMenuItem.SearchKey = "Name";
        }
        protected override void Because_of()
        {
            this.Sut.IsMappingSearch = false;
            this.Sut.NameSearch = "Value";
            this.Sut.Search();
        }

        [TestMethod]
        public void search_should_not_be_null()
        {
            Assert.IsNotNull(this.search);
        }

        [TestMethod]
        public void search_should_contain_single_criteria()
        {
            Assert.AreEqual(1, this.search.SearchFields.Criterias.Count);
        }

        [TestMethod]
        public void search_should_match_on_entity_key()
        {
            var criteria = this.search.SearchFields.Criterias[0];
            AssertSingleSearchCriteria("Name", SearchCondition.Contains, "Value", criteria);
        }
    }

    [TestClass]
    public class when_performing_an_empty_mapping_search : SearchViewModelTestBase
    {
        protected override void Because_of()
        {
            this.Sut.IsMappingSearch = true;
            this.Sut.NameSearch = null;
            this.Sut.Search();
        }

        [TestMethod]
        public void search_should_not_be_null()
        {
            Assert.IsNotNull(this.search);
        }

        [TestMethod]
        public void search_should_contain_single_criteria()
        {
            Assert.AreEqual(1, this.search.SearchFields.Criterias.Count);
        }

        [TestMethod]
        public void search_should_match_on_mapping_value()
        {
            var criteria = this.search.SearchFields.Criterias[0];
            AssertSingleSearchCriteria("MappingValue", SearchCondition.Contains, null, criteria);
        }
    }

    [TestClass]
    public class when_performing_a_mapping_search_with_non_numeric_value : SearchViewModelTestBase
    {
        protected override void Because_of()
        {
            this.Sut.IsMappingSearch = true;
            this.Sut.NameSearch = "abc";
            this.Sut.Search();
        }

        [TestMethod]
        public void search_should_not_be_null()
        {
            Assert.IsNotNull(this.search);
        }

        [TestMethod]
        public void search_should_contain_single_criteria()
        {
            Assert.AreEqual(1, this.search.SearchFields.Criterias.Count);
        }

        [TestMethod]
        public void search_should_match_on_mapping_value()
        {
            var criteria = this.search.SearchFields.Criterias[0];
            AssertSingleSearchCriteria("MappingValue", SearchCondition.Contains, "abc", criteria);
        }
    }

    [TestClass]
    public class when_performing_a_mapping_search_with_numeric_value : SearchViewModelTestBase
    {
        protected override void Because_of()
        {
            this.Sut.IsMappingSearch = true;
            this.Sut.NameSearch = "123";
            this.Sut.Search();
        }

        [TestMethod]
        public void search_should_not_be_null()
        {
            Assert.IsNotNull(this.search);
        }

        [TestMethod]
        public void search_should_contain_two_criteria()
        {
            Assert.AreEqual(2, this.search.SearchFields.Criterias.Count);
        }

        [TestMethod]
        public void search_should_contain_a_mapping_value_criteria()
        {
            var criteria = this.search.SearchFields.Criterias.FirstOrDefault(x => x.Criteria[0].Field == "MappingValue");
            AssertSingleSearchCriteria("MappingValue", SearchCondition.Contains, "123", criteria);
        }

        [TestMethod]
        public void search_should_contain_a_nexus_id_criteria()
        {
            var criteria = this.search.SearchFields.Criterias.FirstOrDefault(x => x.Criteria[0].Field == "MainEntityName.Id");
            AssertSingleSearchCriteria("MainEntityName.Id", SearchCondition.NumericEquals, "123", criteria);
        }
    }

    [TestClass]
    public class when_performing_a_mapping_search_with_numeric_value_and_nexus_source_system : SearchViewModelTestBase
    {
        protected override void Because_of()
        {
            this.Sut.IsMappingSearch = true;
            this.Sut.SourceSystem = "Nexus";
            this.Sut.NameSearch = "123";
            this.Sut.Search();
        }

        [TestMethod]
        public void search_should_not_be_null()
        {
            Assert.IsNotNull(this.search);
        }

        [TestMethod]
        public void search_should_contain_single_criteria()
        {
            Assert.AreEqual(1, this.search.SearchFields.Criterias.Count);
        }

        [TestMethod]
        public void search_should_contain_a_nexus_id_criteria()
        {
            var criteria = this.search.SearchFields.Criterias.FirstOrDefault(x => x.Criteria[0].Field == "MainEntityName.Id");
            AssertSingleSearchCriteria("MainEntityName.Id", SearchCondition.NumericEquals, "123", criteria);
        }
    }

    [TestClass]
    public class when_performing_a_mapping_search_with_non_nexus_source_system : SearchViewModelTestBase
    {
        protected override void Because_of()
        {
            this.Sut.IsMappingSearch = true;
            this.Sut.SourceSystem = "AnotherSystem";
            this.Sut.NameSearch = "123";
            this.Sut.Search();
        }

        [TestMethod]
        public void search_should_not_be_null()
        {
            Assert.IsNotNull(this.search);
        }

        [TestMethod]
        public void search_should_contain_single_criteria()
        {
            Assert.AreEqual(1, this.search.SearchFields.Criterias.Count);
        }

        [TestMethod]
        public void criteria_should_contain_two_sub_criteria()
        {
            Assert.AreEqual(2, this.search.SearchFields.Criterias[0].Criteria.Count);
        }

        [TestMethod]
        public void search_should_match_on_mapping_value()
        {
            var criteria = this.search.SearchFields.Criterias[0].Criteria.FirstOrDefault(x => x.Field == "MappingValue");
            AssertCriteria("MappingValue", SearchCondition.Contains, "123", criteria);
        }

        [TestMethod]
        public void search_should_match_on_source_system()
        {
            var criteria = this.search.SearchFields.Criterias[0].Criteria.FirstOrDefault(x => x.Field == "System.Name");
            AssertCriteria("System.Name", SearchCondition.Equals, "AnotherSystem", criteria);
        }
    }

    [TestClass]
    public class when_performing_a_nexus_id_search_for_derived_class : SearchViewModelTestBase
    {
        protected override void Because_of()
        {
            this.Sut.IsMappingSearch = true;
            this.Sut.SourceSystem = "Nexus";
            this.Sut.NameSearch = "123";
            this.Sut.SelectedMenuItem.BaseEntityName = "BaseEntityName";
            this.Sut.Search();
        }

        [TestMethod]
        public void search_should_not_be_null()
        {
            Assert.IsNotNull(this.search);
        }

        [TestMethod]
        public void search_should_contain_single_criteria()
        {
            Assert.AreEqual(1, this.search.SearchFields.Criterias.Count);
        }

        [TestMethod]
        public void search_should_contain_a_nexus_id_criteria_for_base_class()
        {
            var criteria = this.search.SearchFields.Criterias.FirstOrDefault(x => x.Criteria[0].Field == "BaseEntityName.Id");
            AssertSingleSearchCriteria("BaseEntityName.Id", SearchCondition.NumericEquals, "123", criteria);
        }
    }

    public class SearchViewModelTestBase : TestBase<SearchViewModel>
    {
        protected Search search;

        protected override void Establish_context()
        {
            // explict mapping service mock
            var mappingService = this.RegisterMock<IMappingService>();
            mappingService.Setup(x => x.GetSourceSystemNames()).Returns(new List<string>());

            // explict event publisher mock
            var eventAggregatorExtensionsProvider = this.RegisterMock<IEventAggregatorExtensionsProvider>();
            EventAggregatorExtensions.SetProvider(eventAggregatorExtensionsProvider.Object);

            eventAggregatorExtensionsProvider.Setup(
                x =>
                x.Publish(
                    It.IsAny<IEventAggregator>(), It.Is<SearchRequestEvent>(y => y.EntityName == "MainEntityName"))).
                Callback<IEventAggregator, SearchRequestEvent>((a, b) => this.search = b.Search);

            // create Sut
            base.Establish_context();

            var menuItemViewModel = this.Concrete<MenuItemViewModel>();
            menuItemViewModel.Name = "MainEntityName";
            this.Sut.SelectedMenuItem = menuItemViewModel;
        }

        protected static void AssertSingleSearchCriteria(string mappingvalue, SearchCondition searchCondition, string comparisonValue, SearchCriteria searchCriteria)
        {
            Assert.IsNotNull(searchCriteria);
            var criteria = searchCriteria.Criteria[0];
            AssertCriteria(mappingvalue, searchCondition, comparisonValue, criteria);
        }

        protected static void AssertCriteria(string mappingvalue, SearchCondition searchCondition, string comparisonValue, Criteria criteria)
        {
            Assert.IsNotNull(criteria);
            Assert.AreEqual(mappingvalue, criteria.Field);
            Assert.AreEqual(searchCondition, criteria.Condition);
            Assert.AreEqual(comparisonValue, criteria.ComparisonValue);
        }
    }
}