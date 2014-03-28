namespace Common.Services
{
    using System;

    using Common.Events;
    using Common.Extensions;
    using Common.UI.Uris;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Regions;

    public class NavigationService : INavigationService
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IRegionManager regionManager;

        public NavigationService(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
        }

        public event EventHandler NavigationCleared;

        public void ClearHistory()
        {
            this.regionManager.Regions[RegionNames.MainRegion].NavigationService.Journal.Clear();
            this.regionManager.RequestNavigate(RegionNames.MainRegion, ViewNames.SearchView);
            this.NavigationCleared.FireEvent(this, () => null);
        }

        public void NavigateMain(Uri navigateUri)
        {
            this.eventAggregator.Publish(new StatusEvent(string.Empty));
            this.regionManager.Regions[RegionNames.MainRegion].RequestNavigate(navigateUri);
        }

        public void NavigateMainBack()
        {
            this.eventAggregator.Publish(new StatusEvent(string.Empty));
            this.regionManager.Regions[RegionNames.MainRegion].NavigationService.Journal.GoBack();
        }

        public void NavigateMainBackWithStatus(StatusEvent statusEvent)
        {
            this.NavigateMainBack();
            this.eventAggregator.Publish(statusEvent);
        }

        public void NavigateMainForward()
        {
            this.eventAggregator.Publish(new StatusEvent(string.Empty));
            this.regionManager.Regions[RegionNames.MainRegion].NavigationService.Journal.GoForward();
        }

        public void NavigateToSearch()
        {
            this.ClearHistory();
        }
    }
}