namespace Shell.ViewModels
{
    using System;

    using Common;
    using Common.Extensions;
    using Common.Services;

    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.ViewModel;

    public class MenuItemViewModel : NotificationObject
    {
        private readonly IEventAggregator eventAggregator;

        private string description;
        private string name;
        private DelegateCommand openCommand;
        private DelegateCommand addEntityCommand;

        private Type searchResultsViewType;
        private Uri addEntityUri;
        private string searchKey;
        private string baseEntityName;
        private string searchLabel;

        public MenuItemViewModel(IApplicationCommands commands, INavigationService navigationService, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.OpenCommand = new DelegateCommand(() => commands.OpenView(this.SearchResultsViewType, this.name, RegionNames.MainSearchResultsRegion));
            this.AddEntityCommand = new DelegateCommand(() => navigationService.NavigateMain(this.AddEntityUri));
        }

        public Uri AddEntityUri
        {
            get
            {
                return this.addEntityUri;
            }
            set
            {
                this.addEntityUri = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                this.description = value;
                this.RaisePropertyChanged(() => this.Description);
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.RaisePropertyChanged(() => this.Name);
            }
        }

        public string SplitName
        {
            get
            {
                return name.SplitByCamelCase();
            }
        }

        public DelegateCommand OpenCommand
        {
            get
            {
                return this.openCommand;
            }

            set
            {
                this.openCommand = value;
                this.RaisePropertyChanged(() => this.OpenCommand);
            }
        }

        public DelegateCommand AddEntityCommand
        {
            get
            {
                return this.addEntityCommand;
            }

            set
            {
                this.addEntityCommand = value;
                this.RaisePropertyChanged(() => this.AddEntityCommand);
            }
        }

        public Type SearchResultsViewType
        {
            get
            {
                return this.searchResultsViewType;
            }

            set
            {
                this.searchResultsViewType = value;
                this.RaisePropertyChanged(() => this.SearchResultsViewType);
            }
        }

        public string SearchKey
        {
            get
            {
                return this.searchKey;
            }

            set
            {
                this.searchKey = value;
                this.RaisePropertyChanged(() => this.SearchKey);
            }
        }

        public string SearchLabel
        {
            get { return this.searchLabel; }

            set
            {
                this.searchLabel = value;
                this.RaisePropertyChanged(() => this.SearchLabel);
            }
        }

        public string BaseEntityName
        {
            get
            {
                return this.baseEntityName;
            }
            set
            {
                this.baseEntityName = value;
                this.RaisePropertyChanged(() => this.BaseEntityName);
            }
        }       
    }
}