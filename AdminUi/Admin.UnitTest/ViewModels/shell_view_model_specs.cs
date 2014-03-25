namespace Admin.UnitTest.ViewModels
{
    using System;

    using Admin.UnitTest.Framework;

    using Microsoft.Practices.EnterpriseLibrary.Logging;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Shell.ViewModels;

    [TestClass]
    public class when_a_request_is_made_to_clear_an_error : TestBase<ShellViewModel>
    {
        protected override void Establish_context()
        {
            this.AddConcrete<IEventAggregator, EventAggregator>(new EventAggregator());
            this.RegisterMock<LogWriter>();
            base.Establish_context();
            this.Sut.Error = "my error";
        }

        protected override void Because_of()
        {
            this.Sut.ClearError();
        }

        [TestMethod] 
        public void should_clear_the_error()
        {
            Assert.AreEqual(this.Sut.Error, String.Empty); 
        }
    }
}