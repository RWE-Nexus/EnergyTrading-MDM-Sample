namespace Admin.UnitTest.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Admin.UnitTest.Framework;

    using Common.Services;
    using Common.UI.ViewModels;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_mapping_view_model_is_created : TestBase<MappingAddViewModel>
    {
        private IList<string> response;

        [TestMethod]
        public void should_add_an_empty_source_system_to_the_list()
        {
            Assert.AreEqual(1, this.Sut.SourceSystems.Where(x => x == string.Empty).Count());
        }

        [TestMethod]
        public void should_call_the_mapping_service_for_source_systems()
        {
            Assert.AreEqual(3, this.Sut.SourceSystems.Count());
        }

        [TestMethod]
        public void should_populate_with_the_source_systems_from_the_mapping_service()
        {
            this.Mock<IMappingService>().Verify(x => x.GetSourceSystemNames());
        }

        protected override void Because_of()
        {
            this.RegisterMock<IMappingService>().Setup(x => x.GetSourceSystemNames()).Returns(this.response);
            this.Sut = new MappingAddViewModel(
                this.RegisterMock<IEventAggregator>().Object, 
                this.Mock<IMappingService>().Object, 
                this.RegisterMock<INavigationService>().Object);
        }

        protected override void Establish_context()
        {
            this.response = new List<string> { "Endur", "ADC" };
        }
    }
}