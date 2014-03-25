namespace Shell.Services
{
    using System;
    using System.Collections.Generic;

    using Common.Authorisation;
    using Common.Services;
    using Microsoft.Practices.Unity;
    using Shell.ViewModels;

    public class MenuRegistry : IApplicationMenuRegistry
    {
        private readonly IUnityContainer container;
        private readonly SearchViewModel searchViewModel;
        private readonly ShellViewModel shellViewModel;

        public MenuRegistry(IUnityContainer container, SearchViewModel searchViewModel, ShellViewModel shellViewModel)
        {
            this.container = container;
            this.searchViewModel = searchViewModel;
            this.shellViewModel = shellViewModel;
        }

        public void RegisterMenuItem(string menuItemName, string description, Type searchResultsViewType, Uri addNewEntityUri, string searchKey)
        {
            RegisterMenuItem(menuItemName, description, searchResultsViewType, addNewEntityUri, searchKey, searchKey);
        }

        public void RegisterMenuItem(string menuItemName, string description, Type searchResultsViewType, Uri addNewEntityUri, string searchKey, string searchLabel, string baseEntityName = null)
        {
            var vm = this.container.Resolve<MenuItemViewModel>();
            vm.Name = menuItemName;
            vm.Description = description;
            vm.SearchResultsViewType = searchResultsViewType;
            vm.AddEntityUri = addNewEntityUri;
            vm.SearchKey = searchKey;
            vm.SearchLabel = searchLabel;
            vm.BaseEntityName = baseEntityName;
            this.searchViewModel.AddMenuItem(vm);
            if (AuthorisationHelpers.HasEntityRights(menuItemName))
            {
                this.shellViewModel.AddNewEntityMenuItem(vm);
            }
        }

        public void RegisterEntitySelector(string name, Type type)
        {
            this.shellViewModel.EntitySelectorViews.Add(new KeyValuePair<string, Type>(name, type));
        }
    }
}