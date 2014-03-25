namespace Admin.UnitTest
{
    using System;

    using Admin.UnitTest.Framework;

    using Common.EventAggregator;
    using Common.Extensions;
    using Common.Services;
    using Common.UI.Uris;
    using Common.UI.ViewModels;

    using EnergyTrading.Mdm.Contracts;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Regions;

    using Moq;

    public abstract class mapping_edit_view_model_context : TestBase<MappingEditViewModel>
    {
        protected EntityWithETag<MdmId> Mapping;

        protected EventAggregator eventAggregator;

        protected Mock<IEventAggregatorExtensionsProvider> mockEventAggregator;

        protected override void Establish_context()
        {
            this.mockEventAggregator = new Mock<IEventAggregatorExtensionsProvider>();
            this.eventAggregator = new EventAggregator();

            EventAggregatorExtensions.SetProvider(this.mockEventAggregator.Object);

            this.AddConcrete<IEventAggregator, EventAggregator>(this.eventAggregator);
            base.Establish_context();
            Uri uri = new MappingEditUri(1, "Party", 1);
            this.Mock<IMappingService>()
                .Setup(service => service.GetMapping("Party", 1, 1))
                .Returns(this.Mapping = new EntityWithETag<MdmId>(ObjectMother.CreateMapping(1), "1"));
            this.Sut.OnNavigatedTo(new NavigationContext(null, uri));
        }
    }
}