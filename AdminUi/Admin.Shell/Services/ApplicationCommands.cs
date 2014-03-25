namespace Shell.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common;
    using Common.Events;
    using Common.Services;

    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Unity;

    public class ApplicationCommands : IApplicationCommands
    {
        private readonly IUnityContainer container;

        private readonly IRegionManager regionManager;

        public ApplicationCommands(IRegionManager rm, IUnityContainer container)
        {
            this.regionManager = rm;
            this.container = container;
        }

        public void CloseView(Type viewType)
        {
            CloseView(viewType, RegionNames.MainSearchResultsRegion);
        }

        public void CloseView(Type viewType, string regionName)
        {
            object viewToClose =
                this.regionManager.Regions[regionName].Views.FirstOrDefault(view => view.GetType() == viewType);

            if (viewToClose != null)
            {
                this.regionManager.Regions[regionName].Deactivate(viewToClose);
            }
        }

        public void CloseView(string viewName, string regionName)
        {
            var view = this.container.Resolve<object>(viewName);

            if (this.regionManager.Regions[regionName].Views.Contains(view))
            {
                this.regionManager.Regions[regionName].Remove(view);
            }
        }

        public void OpenView(Type viewType, string activeEntity, string regionName)
        {
            var viewToOpen =
                this.regionManager.Regions[regionName].Views.FirstOrDefault(view => view.GetType() == viewType);

            if (viewToOpen == null)
            {
                viewToOpen = this.container.Resolve(viewType);
                this.regionManager.AddToRegion(regionName, viewToOpen);
            }

            this.regionManager.Regions[regionName].Activate(viewToOpen);
            this.regionManager.Regions[regionName].Context = activeEntity;
        }

        public void OpenView(Type viewType, string activeEntity, string selectedPropertyName, string regionName)
        {
            var viewToOpen =
                this.regionManager.Regions[regionName].Views.FirstOrDefault(view => view.GetType() == viewType);

            if (viewToOpen == null)
            {
                viewToOpen = this.container.Resolve(viewType);
                this.regionManager.AddToRegion(regionName, viewToOpen);
            }

            this.regionManager.Regions[regionName].Activate(viewToOpen);
            this.regionManager.Regions[regionName].Context = new EntitySelectionViewContext(
                activeEntity, 
                selectedPropertyName);
        }

        public void OpenView(string viewName, string regionName, int id, DateTime? validAt)
        {
            var viewToOpen = this.container.Resolve<object>(viewName);
            this.regionManager.Regions[regionName].Context = new Tuple<int, DateTime?>(id, validAt);

            if (!this.regionManager.Regions[regionName].Views.Contains(viewToOpen))
            {
                this.regionManager.AddToRegion(regionName, viewToOpen);
            }
        }

        public void OpenView(string viewName, string regionName, int id, DateTime? validAt, string contextType)
        {
            var viewToOpen = this.container.Resolve<object>(viewName);
            this.regionManager.Regions[regionName].Context = new Tuple<int, DateTime?, string>(id, validAt, contextType);

            if (!this.regionManager.Regions[regionName].Views.Contains(viewToOpen))
            {
                this.regionManager.AddToRegion(regionName, viewToOpen);
            }
        }

        public void OpenView(Type viewType, string regionName, IDictionary<string, string> parameters)
        {
            this.regionManager.Regions[regionName].Context = parameters;

            var viewToOpen =
                this.regionManager.Regions[regionName].Views.FirstOrDefault(view => view.GetType() == viewType);
            if (viewToOpen == null)
            {
                viewToOpen = this.container.Resolve(viewType);
                this.regionManager.AddToRegion(regionName, viewToOpen);
            }

            this.regionManager.Regions[regionName].Activate(viewToOpen);
        }
    }
}