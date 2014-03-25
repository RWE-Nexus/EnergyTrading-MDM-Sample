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
    public class when_an_status_event_is_published : TestBase<ShellViewModel>
    {
        protected override void Establish_context()
        {
            this.AddConcrete<IEventAggregator, EventAggregator>(new EventAggregator());
            this.RegisterMock<LogWriter>();
            base.Establish_context();
        }

        protected override void Because_of()
        {
            this.Concrete<IEventAggregator>().Publish(new StatusEvent("Status Changed"));
        }

        [TestMethod]
        public void should_update_the_stauts_message_on_the_shell()
        {
            Assert.AreEqual(Sut.Status, "Status Changed");
        }
    }
}