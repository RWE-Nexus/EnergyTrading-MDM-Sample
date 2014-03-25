namespace Admin.UnitTest.Events
{
    using System;
    using System.Collections.Generic;
    using Admin.PartyModule.Views;
    using Admin.UnitTest.Framework;

    using Common;
    using Common.Events;
    using Common.Extensions;
    using Common.Services;
    using Microsoft.Practices.EnterpriseLibrary.Logging;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using Shell.ViewModels;

    [TestClass]
    public class when_an_entity_select_event_is_published : TestBase<ShellViewModel>
    {
        protected override void Establish_context()
        {
            this.AddConcrete<IEventAggregator, EventAggregator>(new EventAggregator());
            this.RegisterMock<LogWriter>();
            base.Establish_context();
            this.Sut.EntitySelectorViews.Add(new KeyValuePair<string, Type>("Party", typeof(PartySelectorView)));
        }

        protected override void Because_of()
        {
            this.Concrete<IEventAggregator>().Publish(new EntitySelectEvent("Party", "SourceParty"));
        }

        [TestMethod]
        public void should_open_the_correct_entity_selector()
        {
            this.Mock<IApplicationCommands>().Verify(commands => commands.OpenView(typeof(PartySelectorView), "Party", "SourceParty", RegionNames.EntitySelectorRegion));
        }

        [TestMethod]
        public void should_tell_the_shell_that_an_entity_selector_is_open()
        {
            Assert.AreEqual(true, this.Sut.SelectEntity);
        }
    }
}