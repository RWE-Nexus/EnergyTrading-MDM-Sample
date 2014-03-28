namespace Admin.UnitTest.Extensions
{
    using System.Threading;

    using Common.EventAggregator;
    using Common.Events;
    using Common.Extensions;

    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.MDM.Contracts.Sample;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class when_a_request_is_made_to_execute_an_async_call
    {
        private IEventAggregator eventAggregator;

        private Mock<IEventAggregatorExtensionsProvider> mockEventAggregator;

        private Mock<IMdmService> mockService;

        private bool serviceMethodExecuted;

        [TestInitialize]
        public void TestInitialize()
        {
            this.Establish_context();
            this.Becuase_of();
        }

        [TestMethod]
        public void should_execute_the_service_call()
        {
            Assert.IsTrue(this.serviceMethodExecuted);
        }

        [TestMethod]
        public void should_tell_the_ui_that_is_has_finished_processing_and_is_not_busy()
        {
            this.mockEventAggregator.Verify(p => p.Publish(this.eventAggregator, It.Is<BusyEvent>(e => e.IsBusy)));
        }

        [TestMethod]
        public void should_tell_the_ui_that_it_is_busy()
        {
            this.mockEventAggregator.Verify(p => p.Publish(this.eventAggregator, It.Is<BusyEvent>(e => !e.IsBusy)));
        }

        protected void Becuase_of()
        {
            this.mockService.Object.ExecuteAsync(
                () => new WebResponse<Person>(), 
                response => this.serviceMethodExecuted = true, 
                this.eventAggregator);

            // Allows us to test the work complete part of the call 
            Thread.Sleep(100);
        }

        protected void Establish_context()
        {
            this.mockService = new Mock<IMdmService>();
            this.mockEventAggregator = new Mock<IEventAggregatorExtensionsProvider>();
            this.eventAggregator = new EventAggregator();

            EventAggregatorExtensions.SetProvider(this.mockEventAggregator.Object);
        }
    }

    [TestClass]
    public class when_a_request_is_made_to_execute_an_async_call_and_the_response_is_not_valid
    {
        private IEventAggregator eventAggregator;

        private Mock<IEventAggregatorExtensionsProvider> mockEventAggregator;

        private Mock<IMdmService> mockService;

        private bool serviceMethodExecuted;

        [TestInitialize]
        public void TestInitialize()
        {
            this.Establish_context();
            this.Becuase_of();
        }

        [TestMethod]
        public void should_not_execute_the_success_action()
        {
            Assert.IsFalse(this.serviceMethodExecuted);
        }

        [TestMethod]
        public void should_tell_the_ui_that_is_has_finished_processing_and_is_not_busy()
        {
            this.mockEventAggregator.Verify(p => p.Publish(this.eventAggregator, It.Is<BusyEvent>(e => e.IsBusy)));
        }

        [TestMethod]
        public void should_tell_the_ui_that_it_is_busy()
        {
            this.mockEventAggregator.Verify(p => p.Publish(this.eventAggregator, It.Is<BusyEvent>(e => !e.IsBusy)));
        }

        [TestMethod]
        public void should_tell_the_ui_there_has_been_an_exception()
        {
            this.mockEventAggregator.Verify(p => p.Publish(this.eventAggregator, It.IsAny<ErrorEvent>()));
        }

        protected void Becuase_of()
        {
            this.mockService.Object.ExecuteAsync(
                () => new WebResponse<Person> { IsValid = false }, 
                response => this.serviceMethodExecuted = true, 
                this.eventAggregator);
        }

        protected void Establish_context()
        {
            this.mockService = new Mock<IMdmService>();
            this.mockEventAggregator = new Mock<IEventAggregatorExtensionsProvider>();
            this.eventAggregator = new EventAggregator();

            EventAggregatorExtensions.SetProvider(this.mockEventAggregator.Object);
        }
    }
}