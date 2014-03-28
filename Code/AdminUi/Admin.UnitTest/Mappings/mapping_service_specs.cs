namespace Admin.UnitTest
{
    using System.Collections.Generic;

    using Admin.UnitTest.Framework;

    using Common.EventAggregator;
    using Common.Events;
    using Common.Extensions;
    using Common.Services;

    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Mdm.Contracts;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class when_a_request_is_made_for_source_systems : TestBase<MappingService>
    {
        private string baseUri = "baseUri/";

        private IList<string> response;

        [TestMethod]
        public void should_ask_the_message_requester_for_a_list_of_source_systems()
        {
            this.Mock<IMessageRequester>().Verify(x => x.Request<SourceSystemList>(this.baseUri + "SourceSystem/List"));
        }

        [TestMethod]
        public void should_return_the_correct_number_of_systems()
        {
            Assert.AreEqual(2, this.response.Count);
        }

        [TestMethod]
        public void should_return_the_system_names_from_the_service()
        {
            Assert.AreEqual("Test", this.response[0]);
        }

        protected override void Because_of()
        {
            this.response = this.Sut.GetSourceSystemNames();
        }

        protected override void Establish_context()
        {
            this.Sut = new MappingService(
                this.baseUri, 
                this.RegisterMock<IMessageRequester>().Object, 
                this.RegisterMock<IEventAggregator>().Object);
            var response = new WebResponse<SourceSystemList>
                               {
                                   Message =
                                       new SourceSystemList
                                           {
                                               new SourceSystem
                                                   {
                                                       Details
                                                           =
                                                           new SourceSystemDetails
                                                               {
                                                                   Name
                                                                       =
                                                                       "Test"
                                                               }
                                                   }, 
                                               new SourceSystem
                                                   {
                                                       Details
                                                           =
                                                           new SourceSystemDetails
                                                               {
                                                                   Name
                                                                       =
                                                                       "Test2"
                                                               }
                                                   }
                                           }
                               };
            this.Mock<IMessageRequester>().Setup(x => x.Request<SourceSystemList>(It.IsAny<string>())).Returns(response);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_source_systems_and_an_error_occurs_in_the_serivce : TestBase<MappingService>
    {
        private string baseUri = "baseUri/";

        private EventAggregator eventAggregator;

        private Fault falutFromService;

        private Mock<IEventAggregatorExtensionsProvider> mockEventAggregator;

        private IList<string> response;

        [TestMethod]
        public void should_publish_an_error_event_for_the_fault()
        {
            this.mockEventAggregator.Verify(p => p.Publish(this.eventAggregator, It.IsAny<ErrorEvent>()));
        }

        [TestMethod]
        public void should_return_no_systems()
        {
            Assert.AreEqual(0, this.response.Count);
        }

        protected override void Because_of()
        {
            this.response = this.Sut.GetSourceSystemNames();
        }

        protected override void Establish_context()
        {
            this.mockEventAggregator = new Mock<IEventAggregatorExtensionsProvider>();
            this.eventAggregator = new EventAggregator();

            EventAggregatorExtensions.SetProvider(this.mockEventAggregator.Object);

            this.AddConcrete<IEventAggregator, EventAggregator>(this.eventAggregator);
            this.Sut = new MappingService(
                this.baseUri, 
                this.RegisterMock<IMessageRequester>().Object, 
                this.eventAggregator);
            this.falutFromService = new Fault();
            var response = new WebResponse<SourceSystemList> { IsValid = false, Fault = this.falutFromService };
            this.Mock<IMessageRequester>().Setup(x => x.Request<SourceSystemList>(It.IsAny<string>())).Returns(response);
        }
    }
}