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
    public class when_an_can_save_event_is_published_because_a_save_can_be_made : TestBase<ShellViewModel>
    {
        protected override void Establish_context()
        {
            this.AddConcrete<IEventAggregator, EventAggregator>(new EventAggregator());
            this.RegisterMock<LogWriter>();
            base.Establish_context();
        }

        protected override void Because_of()
        {
            this.Concrete<IEventAggregator>().Publish(new CanSaveEvent(true));
        }

        [TestMethod]
        public void should_update_the_shell_to_allow_for_saving()
        {
            Assert.AreEqual(Sut.CanSave, true);
        }
    }

    [TestClass]
    public class when_an_can_save_event_is_published_because_a_save_can_not_be_made : TestBase<ShellViewModel>
    {
        protected override void Establish_context()
        {
            this.AddConcrete<IEventAggregator, EventAggregator>(new EventAggregator());
            this.RegisterMock<LogWriter>();
            base.Establish_context();
        }

        protected override void Because_of()
        {
            this.Concrete<IEventAggregator>().Publish(new CanSaveEvent(false));
        }

        [TestMethod]
        public void should_update_the_shell_to_allow_for_saving()
        {
            Assert.AreEqual(Sut.CanSave, false);
        }
    }
}