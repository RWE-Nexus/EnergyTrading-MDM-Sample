namespace Admin.UnitTest
{
    /*
        using System;
        using Admin.PartyModule.Uris;
        using Admin.PartyModule.ViewModels;
        using Admin.UnitTest.Framework;
        using Common.Events;
        using Common.Extensions;
        using Common.Services;
        using Common.UI.Uris;
        using Common.UI.ViewModels;
        using Microsoft.Practices.Prism.Events;
        using Microsoft.Practices.Prism.Regions;
        using Microsoft.VisualStudio.TestTools.UnitTesting;
        using Moq;
        using EnergyTrading.Mdm.Client.Services;
        using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
        using Shell.ViewModels;

        public abstract class party_edit_view_model_context : TestBase<PartyEditViewModel>
        {
            protected override void Establish_context()
            {
                this.AddConcrete<IEventAggregator, EventAggregator>(new EventAggregator());
                base.Establish_context();
                this.Mock<IMdmService>().Setup(service => service.Get<Party>It.IsAny<int>(), It.IsAny<DateTime>())).
                    Returns(new EntityWithETag<Party>(ObjectMother.Create<Party>(), "1"));

                Uri uri = new PartyEditUri(1, DateTime.Now);

                this.Sut.OnNavigatedTo(new NavigationContext(null, uri));
            }
        }

        [TestClass]
        public class when_a_request_is_made_to_save_an_edited_party : party_edit_view_model_context
        {
            [TestMethod]
            public void should_ask_the_entity_service_to_update_the_selected_party()
            {
                int id = this.Sut.Party.Id.Value;
                this.Mock<IUiEntityService<Party>>().Verify(
                    service =>
                    service.Update(
                        id, 
                        It.Is<EntityWithETag<Party>>(
                            x =>
                            x.Object.Details.Name == this.Sut.Party.Name && x.Object.Details.Role == this.Sut.Party.Role &&
                            x.Object.Details.FaxNumber == this.Sut.Party.FaxNumber &&
                            x.Object.Details.TelephoneNumber == this.Sut.Party.TelephoneNumber &&
                            x.Object.Nexus.StartDate == this.Sut.Party.Start && x.Object.Nexus.EndDate == this.Sut.Party.End)));
            }

            protected override void Because_of()
            {
                this.Concrete<IEventAggregator>().Publish(new SaveEvent());
            }
        }

        [TestClass]
        public class when_a_request_is_made_to_navigate_to_a_mapping : party_edit_view_model_context
        {
            [TestMethod]
            public void should_call_the_navigation_service_to_requst_a_move_to_the_mapping_view_for_the_selected_mapping()
            {
                this.Mock<INavigationService>().Verify(
                    service =>
                    service.NavigateMain(It.Is<Uri>(uri => uri.OriginalString == new MappingEditUri(1, 1).OriginalString)));
            }

            protected override void Because_of()
            {
                this.Sut.NavigateToDetail();
            }

            protected override void Establish_context()
            {
                base.Establish_context();
                this.Sut.SelectedMapping = new MappingViewModel(new EntityWithETag<MdmId>(new MdmId() { MappingId = 1 }, "1"), this.Concrete<IEventAggregator>());
            }
        }

        [TestClass]
        public class when_a_request_is_made_to_create_a_new_mapping : party_edit_view_model_context
        {
            [TestMethod]
            public void should_ask_the_navigation_service_to_navigate_to_the_create_mapping_view_for_the_party_being_edited()
            {
                this.Mock<INavigationService>().Verify(
                    service =>
                    service.NavigateMain(It.Is<Uri>(uri => uri.OriginalString == new MappingAddUri(1).OriginalString)));
            }

            protected override void Because_of()
            {
                this.Concrete<IEventAggregator>().Publish(new CreateEvent());
            }
        }
    */
}