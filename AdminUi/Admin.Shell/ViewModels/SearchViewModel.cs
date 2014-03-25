namespace Shell.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Common;
    using Common.Authorisation;
    using Common.Events;
    using Common.Extensions;
    using Common.Services;
    using Common.UI;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;
    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Search;

    public class SearchViewModel : NotificationObject, INavigationAware
    {
        private const string NexusSourceSystem = "Nexus";

        private readonly IApplicationCommands applicationCommands;
        private readonly INavigationService navigationService;
        private readonly IMappingService mappingService;
        private readonly IEventAggregator eventAggregator;
        private DateTime asOf;
        private bool displayWelcome;
        private bool isMappingSearch;
        private string nameSearch;
        private int? searchCountFound;
        private string mappingNameSearch;

        private MenuItemViewModel selectedMenuItem;
        private IList<string> sourceSystems;
        private string sourceSystem;

        public SearchViewModel(IEventAggregator eventAggregator, IApplicationCommands applicationCommands, INavigationService navigationService, IMappingService mappingService)
        {
            this.eventAggregator = eventAggregator;
            this.applicationCommands = applicationCommands;
            this.navigationService = navigationService;
            this.mappingService = mappingService;
            this.MenuItems = new ObservableCollection<MenuItemViewModel>();

            this.DisplayWelcome = true;
            this.AsOf = SystemTime.UtcNow().Date;

            this.SourceSystems = SourceSystemList(this.mappingService);
          
            this.SourceSystem = string.Empty;
        }

        private static IList<string> SourceSystemList(IMappingService mappingService)
        {
            var sourceSystemList = mappingService.GetSourceSystemNames();
            sourceSystemList.Insert(0, string.Empty);
            sourceSystemList.Add(NexusSourceSystem);
            sourceSystemList = sourceSystemList.OrderBy(x => x).ToList();
            return sourceSystemList;
        }

        public IList<string> SourceSystems
        {
            get
            {
                return this.sourceSystems;
            } 
            set
            {
                this.sourceSystems = value;
                this.RaisePropertyChanged(() => this.SourceSystems);
            }
        }


        public DateTime AsOf
        {
            get
            {
                return this.asOf;
            }

            set
            {
                this.asOf = value;
                this.RaisePropertyChanged(() => this.AsOf);
            }
        }

        public bool DisplayWelcome
        {
            get
            {
                return this.displayWelcome;
            }

            set
            {
                this.displayWelcome = value;
                this.RaisePropertyChanged(() => this.DisplayWelcome);
            }
        }

        public string SourceSystem
        {
            get
            {
                return this.sourceSystem;
            }

            set
            {
                this.sourceSystem = value;
                this.RaisePropertyChanged(() => this.SourceSystem);
            }
        }

        public bool IsMappingSearch
        {
            get
            {
                return this.isMappingSearch;
            }

            set
            {
                if (!value)
                {
                    this.SourceSystem = string.Empty;
                }

                this.isMappingSearch = value;
                this.RaisePropertyChanged(() => this.IsMappingSearch);
            }
        }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }

        public string NameSearch
        {
            get
            {
                return this.nameSearch;
            }

            set
            {
                this.nameSearch = value;
                this.RaisePropertyChanged(() => this.NameSearch);
            }
        }

        public string MappingNameSearch
        {
            get
            {
                return this.mappingNameSearch;
            }

            set
            {
                this.mappingNameSearch = value;
                this.RaisePropertyChanged(() => this.MappingNameSearch);
            }
        }

        public int? SearchCountFound
        {
            get
            {
                return this.searchCountFound;
            }

            set
            {
                this.searchCountFound = value;
                this.RaisePropertyChanged(() => this.SearchCountFound);
            }
        }

        public MenuItemViewModel SelectedMenuItem
        {
            get
            {
                return this.selectedMenuItem;
            }

            set
            {
                this.selectedMenuItem = value;
                this.RaisePropertyChanged(() => this.SelectedMenuItem);
            }
        }

        public void AddMenuItem(MenuItemViewModel menuItem)
        {
            this.MenuItems.Add(menuItem);

            if (MenuItems.Count == 1 || SelectedMenuItem != MenuItems[0])
            {
                this.SelectedMenuItem = this.MenuItems[0];
                eventAggregator.Publish(new CanCreateNewChangeEvent(AuthorisationHelpers.HasEntityRights(SelectedMenuItem.Name)));
            }
        }

        public void EntityChanged()
        {
            this.SearchCountFound = null;
            this.navigationService.ClearHistory();
            this.SelectedMenuItem.OpenCommand.Execute();
            eventAggregator.Publish(new CanCreateNewChangeEvent(AuthorisationHelpers.HasEntityRights(SelectedMenuItem.Name)));
        }

        public void Search()
        {
            var search = this.IsMappingSearch ? this.BuildMappingSearch() : this.BuildNameSearch();

            this.SearchCountFound = null;
            this.SelectedMenuItem.OpenCommand.Execute();
            this.eventAggregator.Publish(new SearchRequestEvent(search, this.SelectedMenuItem.Name));
        }

        private Search BuildMappingSearch()
        {
            var search = SearchBuilder.CreateSearch(SearchCombinator.Or, isMappingSearch: true);
            search.AsOf = this.AsOf;

            if (this.ShouldAddMappingValueSearchCriteria())
            {
                this.AddMappingValueSearchCriteria(search);
            }

            if (this.ShouldAddMdmIdSearchCriteria())
            {
                this.AddMdmIdSearchCriteria(search);                
            }

            return search;
        }

        private bool ShouldAddMappingValueSearchCriteria()
        {
            // always add mapping value if no source system specified
            // otherwise don't add it if source system is Nexus
            return (string.IsNullOrEmpty(this.SourceSystem) || this.SourceSystem != NexusSourceSystem);
        }

        private void AddMappingValueSearchCriteria(Search search)
        {
            var searchCriteria = search.AddSearchCriteria(SearchCombinator.And).AddCriteria(
                "MappingValue", SearchCondition.Contains, this.NameSearch);

            if (!string.IsNullOrEmpty(this.SourceSystem))
            {
                searchCriteria.AddCriteria("System.Name", SearchCondition.Equals, this.SourceSystem);
            }
        }

        private bool ShouldAddMdmIdSearchCriteria()
        {
            // don't do a nexus search if source system is not Nexus
            if (!string.IsNullOrEmpty(this.SourceSystem) && this.SourceSystem != NexusSourceSystem) return false;

            // otherwise do a nexus search if search value is numeric
            int id;
            return int.TryParse(this.NameSearch, out id);
        }

        private void AddMdmIdSearchCriteria(Search search)
        {
            search.AddSearchCriteria(SearchCombinator.And).AddCriteria(
                this.EntityName() + ".Id", SearchCondition.NumericEquals, this.NameSearch);
        }

        private string EntityName()
        {
            return string.IsNullOrEmpty(this.SelectedMenuItem.BaseEntityName)
                       ? this.SelectedMenuItem.Name
                       : this.SelectedMenuItem.BaseEntityName;
        }

        private Search BuildNameSearch()
        {
            var search = SearchBuilder.CreateSearch();
            search.AsOf = this.AsOf;

            if (!string.IsNullOrEmpty(this.NameSearch))
            {
                search.AddSearchCriteria(SearchCombinator.And).AddCriteria(
                    this.SelectedMenuItem.SearchKey, SearchCondition.Contains, this.NameSearch);
            }

            return search;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            this.eventAggregator.Unsubscribe<SearchResultsFound>(this.UpdateSearchResults);
            this.eventAggregator.Unsubscribe<CreateEvent>(this.CreateEntity);
            this.applicationCommands.CloseView(this.SelectedMenuItem.SearchResultsViewType);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.eventAggregator.Subscribe<SearchResultsFound>(this.UpdateSearchResults);
            this.eventAggregator.Subscribe<CreateEvent>(this.CreateEntity);

            if (this.SelectedMenuItem != null)
            {
                this.applicationCommands.OpenView(
                    this.SelectedMenuItem.SearchResultsViewType, this.SelectedMenuItem.Name, RegionNames.MainSearchResultsRegion);
                eventAggregator.Publish(new CanCreateNewChangeEvent(AuthorisationHelpers.HasEntityRights(SelectedMenuItem.Name)));
            }
        }

        private void CreateEntity(CreateEvent obj)
        {
            if (this.SelectedMenuItem.Name != "Calendar")
            {
                this.navigationService.NavigateMain(new Uri(this.SelectedMenuItem.Name + "AddView", UriKind.Relative));
                return;
            }

            this.eventAggregator.Publish(new StatusEvent(Message.CalendarAddNotSupported));
        }

        private void UpdateSearchResults(SearchResultsFound results)
        {
            this.DisplayWelcome = false;
            this.SearchCountFound = results.Count;
        }
    }
}