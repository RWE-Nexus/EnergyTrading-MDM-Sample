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
    public class when_an_entity_selected_event_is_published : TestBase<ShellViewModel>
    {
        [TestMethod]
        public void should_tell_the_shell_that_an_entity_has_been_selected()
        {
            Assert.AreEqual(false, this.Sut.SelectEntity);
        }

        protected override void Because_of()
        {
            this.Concrete<IEventAggregator>().Publish(new EntitySelectedEvent("Party", 1, "value"));
        }

        protected override void Establish_context()
        {
            this.AddConcrete<IEventAggregator, EventAggregator>(new EventAggregator());
            this.RegisterMock<LogWriter>();
            base.Establish_context();
        }
    }
}