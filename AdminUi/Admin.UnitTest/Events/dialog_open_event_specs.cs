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
    public class when_an_dialog_open_event_is_published_with_a_status_of_open : TestBase<ShellViewModel>
    {
        [TestMethod]
        public void should_update_the_shell_to_be_aware_a_dialog_is_open()
        {
            Assert.AreEqual(Sut.DialogOpen, true);
        }

        protected override void Because_of()
        {
            this.Concrete<IEventAggregator>().Publish(new DialogOpenEvent(true));
        }

        protected override void Establish_context()
        {
            this.AddConcrete<IEventAggregator, EventAggregator>(new EventAggregator());
            this.RegisterMock<LogWriter>();
            base.Establish_context();
        }
    }

    [TestClass]
    public class when_an_dialog_open_event_is_published_with_a_status_of_closed : TestBase<ShellViewModel>
    {
        [TestMethod]
        public void should_update_the_shell_to_be_aware_a_dialog_is_open()
        {
            Assert.AreEqual(Sut.DialogOpen, false);
        }

        protected override void Because_of()
        {
            this.Concrete<IEventAggregator>().Publish(new DialogOpenEvent(false));
        }

        protected override void Establish_context()
        {
            this.AddConcrete<IEventAggregator, EventAggregator>(new EventAggregator());
            this.RegisterMock<LogWriter>();
            base.Establish_context();
        }
    }
}