namespace Admin.UnitTest.Events
{
    using Admin.UnitTest.Framework;

    using Common.Events;
    using Common.Extensions;

    using Microsoft.Practices.EnterpriseLibrary.Logging;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Shell.ViewModels;

    [TestClass]
    public class when_an_busy_event_is_published_with_a_busy_status : TestBase<ShellViewModel>
    {
        [TestMethod]
        public void should_update_the_shell_to_be_busy()
        {
            Assert.AreEqual(Sut.IsBusy, true);
        }

        protected override void Because_of()
        {
            this.Concrete<IEventAggregator>().Publish(new BusyEvent(true));
        }

        protected override void Establish_context()
        {
            this.AddConcrete<IEventAggregator, EventAggregator>(new EventAggregator());
            this.RegisterMock<LogWriter>();
            base.Establish_context();
        }
    }

    [TestClass]
    public class when_an_busy_event_is_published_with_a_not_busy_status : TestBase<ShellViewModel>
    {
        [TestMethod]
        public void should_update_the_shell_to_be_not_busy()
        {
            Assert.AreEqual(Sut.IsBusy, false);
        }

        protected override void Because_of()
        {
            this.Concrete<IEventAggregator>().Publish(new BusyEvent(false));
        }

        protected override void Establish_context()
        {
            this.AddConcrete<IEventAggregator, EventAggregator>(new EventAggregator());
            this.RegisterMock<LogWriter>();
            base.Establish_context();
        }
    }
}