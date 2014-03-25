namespace Shell.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    using Common;
    using Common.Commands;
    using Common.Events;
    using Common.Extensions;
    using Common.Services;
    using Common.UI.Views;

    using Microsoft.Practices.EnterpriseLibrary.Logging;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;

    using Shell.Services;

    public class ShellViewModel : NotificationObject
    {
        private readonly IApplicationCommands applicationCommands;

        private readonly IEventAggregator eventAggregator;

        private readonly LogWriter logWriter;

        private readonly INavigationService navigationService;

        private readonly IRegionManager regionManager;

        private bool canClone;

        private bool canCreateNew;

        private bool canGoBack;

        private bool canGoForward;

        private bool canGoToSearch;

        private bool canSave;

        private RelayCommand cloneCommand;

        private bool dialogOpen;

        private string error;

        private string helpToolTip;

        private bool isBusy;

        private IRegionNavigationJournal journal;

        private RelayCommand navigateBackCommand;

        private RelayCommand navigateForwardCommand;

        private RelayCommand navigateToSearchCommand;

        private RelayCommand newCommand;

        private RelayCommand openHelpCommand;

        private RelayCommand saveCommand;

        private bool selectEntity;

        private Brush selectedServerColor;

        private string selectedServerMenuText;

        private ObservableCollection<string> serverList;

        private RelayCommand serverSelectedCommand;

        private bool showUpdateMappingRegion;

        private string status;

        public ShellViewModel(
            IRegionManager regionManager, 
            IEventAggregator eventAggregator, 
            INavigationService navigationService, 
            IApplicationCommands applicationCommands, 
            LogWriter logWriter)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.navigationService = navigationService;
            this.applicationCommands = applicationCommands;
            this.logWriter = logWriter;
            this.EntitySelectorViews = new List<KeyValuePair<string, Type>>();
            this.ExitCommand = new DelegateCommand<object>(this.AppExit, this.CanAppExit);
            this.eventAggregator.Subscribe<BusyEvent>(this.SetBusy);
            this.eventAggregator.Subscribe<DialogOpenEvent>(this.DialogOpened);
            this.eventAggregator.Subscribe<StatusEvent>(this.UpdateStatus);
            this.eventAggregator.Subscribe<ErrorEvent>(this.ShowError);
            this.eventAggregator.Subscribe<CanSaveEvent>(this.UpdateCanSave);
            this.eventAggregator.Subscribe<EntitySelectEvent>(this.ShowSelectEntity);
            this.eventAggregator.Subscribe<EntitySelectedEvent>(this.HideSelectEntity);
            this.eventAggregator.Subscribe<MappingUpdateEvent>(this.ShowUpdateMapping);
            this.eventAggregator.Subscribe<MappingUpdatedEvent>(this.HideUpdateMapping);
            this.eventAggregator.Subscribe<CanCreateNewChangeEvent>(this.CanCreateNewChange);
            this.eventAggregator.Subscribe<CanCloneChangeEvent>(this.CanCloneChange);
            this.eventAggregator.Subscribe<ConfirmMappingDeleteEvent>(this.ConfirmMappingDelete);
            this.eventAggregator.Subscribe<MappingDeleteConfirmedEvent>(this.MappingDeleteConfirmed);
            this.NewEntityMenuItems = new ObservableCollection<MenuItemViewModel>();

            this.serverList = new ObservableCollection<string>();
            this.SetServerList();

            this.HelpToolTip = "Help Documentation (" + Assembly.GetExecutingAssembly().GetName().Version + ")";
        }

        public bool CanClone
        {
            get
            {
                return this.canClone;
            }

            set
            {
                this.canClone = value;
                this.RaisePropertyChanged(() => this.CanClone);
            }
        }

        public bool CanCreateNew
        {
            get
            {
                return this.canCreateNew;
            }

            set
            {
                this.canCreateNew = value;
                this.RaisePropertyChanged(() => this.CanCreateNew);
            }
        }

        public bool CanGoBack
        {
            get
            {
                return this.canGoBack;
            }

            set
            {
                this.canGoBack = value;
                this.RaisePropertyChanged(() => this.CanGoBack);
            }
        }

        public bool CanGoForward
        {
            get
            {
                return this.canGoForward;
            }

            set
            {
                this.canGoForward = value;
                this.RaisePropertyChanged(() => this.CanGoForward);
            }
        }

        public bool CanGoToSearch
        {
            get
            {
                return this.canGoToSearch;
            }

            set
            {
                this.canGoToSearch = value;
                this.RaisePropertyChanged(() => this.CanGoToSearch);
            }
        }

        public bool CanSave
        {
            get
            {
                return this.canSave;
            }

            set
            {
                this.canSave = value;
                this.RaisePropertyChanged(() => this.CanSave);
            }
        }

        public ICommand CloneCommand
        {
            get
            {
                if (this.cloneCommand == null)
                {
                    this.cloneCommand = new RelayCommand(param => this.NavigateToClone(), param => this.CanClone);
                }

                return this.cloneCommand;
            }
        }

        public bool DialogOpen
        {
            get
            {
                return this.dialogOpen;
            }

            set
            {
                this.dialogOpen = value;
                this.RaisePropertyChanged(() => this.DialogOpen);
            }
        }

        public IList<KeyValuePair<string, Type>> EntitySelectorViews { get; set; }

        public string Error
        {
            get
            {
                return this.error;
            }

            set
            {
                this.error = value;
                this.RaisePropertyChanged(() => this.Error);
            }
        }

        public DelegateCommand<object> ExitCommand { get; private set; }

        public string HelpToolTip
        {
            get
            {
                return this.helpToolTip;
            }

            set
            {
                this.helpToolTip = value;
                this.RaisePropertyChanged(() => this.HelpToolTip);
            }
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                this.isBusy = value;
                this.RaisePropertyChanged(() => this.IsBusy);
            }
        }

        public ICommand NavigateBackCommand
        {
            get
            {
                if (this.navigateBackCommand == null)
                {
                    this.navigateBackCommand = new RelayCommand(param => this.NavigateBack(), param => this.CanGoBack);
                }

                return this.navigateBackCommand;
            }
        }

        public ICommand NavigateForwardCommand
        {
            get
            {
                if (this.navigateForwardCommand == null)
                {
                    this.navigateForwardCommand = new RelayCommand(
                        param => this.NavigateForward(), 
                        param => this.CanGoForward);
                }

                return this.navigateForwardCommand;
            }
        }

        public ICommand NavigateToSearchCommand
        {
            get
            {
                if (this.navigateToSearchCommand == null)
                {
                    this.navigateToSearchCommand = new RelayCommand(
                        parma => this.NavigateToSearch(), 
                        param => this.CanGoToSearch);
                }

                return this.navigateToSearchCommand;
            }
        }

        public ICommand NewCommand
        {
            get
            {
                if (this.newCommand == null)
                {
                    this.newCommand = new RelayCommand(param => this.NavigateToNew(), param => this.CanCreateNew);
                }

                return this.newCommand;
            }
        }

        public ObservableCollection<MenuItemViewModel> NewEntityMenuItems { get; set; }

        public ICommand OpenHelpCommand
        {
            get
            {
                if (this.openHelpCommand == null)
                {
                    this.openHelpCommand =
                        new RelayCommand(param => Process.Start(ConfigurationManager.AppSettings["help_document"]));
                }

                return this.openHelpCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new RelayCommand(param => this.Save(), param => this.CanSave);
                }

                return this.saveCommand;
            }
        }

        public bool SelectEntity
        {
            get
            {
                return this.selectEntity;
            }

            set
            {
                this.selectEntity = value;
                this.RaisePropertyChanged(() => this.SelectEntity);
            }
        }

        public Brush SelectedServerColor
        {
            get
            {
                return this.selectedServerColor;
            }

            set
            {
                this.selectedServerColor = value;
                this.RaisePropertyChanged(() => this.SelectedServerColor);
            }
        }

        public string SelectedServerMenuText
        {
            get
            {
                return this.selectedServerMenuText;
            }

            set
            {
                this.selectedServerMenuText = value;
                this.RaisePropertyChanged(() => this.SelectedServerMenuText);
            }
        }

        public ObservableCollection<string> ServerList
        {
            get
            {
                return this.serverList;
            }

            set
            {
                this.serverList = value;
                this.RaisePropertyChanged(() => this.ServerList);
            }
        }

        public ICommand ServerSelectedCommand
        {
            get
            {
                if (this.serverSelectedCommand == null)
                {
                    this.serverSelectedCommand = new RelayCommand(x => { }, y => true);
                }

                return this.serverSelectedCommand;
            }
        }

        public bool ShowUpdateMappingRegion
        {
            get
            {
                return this.showUpdateMappingRegion;
            }

            set
            {
                this.showUpdateMappingRegion = value;
                this.RaisePropertyChanged(() => this.ShowUpdateMappingRegion);
            }
        }

        public string Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
                this.RaisePropertyChanged(() => this.Status);
            }
        }

        public void AddNewEntityMenuItem(MenuItemViewModel menuItem)
        {
            this.NewEntityMenuItems.Add(menuItem);
        }

        public void ClearError()
        {
            this.Error = string.Empty;
        }

        public void Initialise()
        {
            this.canSave = false;
            this.journal = this.regionManager.Regions[RegionNames.MainRegion].NavigationService.Journal;
            this.regionManager.Regions[RegionNames.MainRegion].NavigationService.Navigated += (sender, args) =>
                {
                    this.CanGoBack = this.journal.CanGoBack;
                    this.CanGoForward = this.journal.CanGoForward;

                    // this.CanCreateNew = true;
                    this.CanGoToSearch = this.journal.CanGoBack;
                };

            this.navigationService.NavigationCleared += (x, y) =>
                {
                    this.CanGoBack = this.journal.CanGoBack;
                    this.CanGoForward = this.journal.CanGoForward;

                    // this.CanCreateNew = true;
                    this.CanGoToSearch = false;
                };

            WindowPosition.Reposition();
        }

        public void NavigateBack()
        {
            this.navigationService.NavigateMainBack();
        }

        public void NavigateForward()
        {
            this.navigationService.NavigateMainForward();
        }

        public void NavigateToClone()
        {
            this.eventAggregator.Publish(new CloneEvent());
        }

        public void NavigateToNew()
        {
            this.eventAggregator.Publish(new CreateEvent());
        }

        public void NavigateToSearch()
        {
            this.navigationService.NavigateToSearch();
        }

        public void Save()
        {
            this.eventAggregator.Publish(new SaveEvent());
        }

        public void ServerChanged()
        {
            string args;
            args = string.Format(
                "{0} {1} {2} {3} {4}", 
                ConfigurationManager.AppSettings["menu_service_" + this.SelectedServerMenuText], 
                Application.Current.MainWindow.Left, 
                Application.Current.MainWindow.Top, 
                Application.Current.MainWindow.Width, 
                Application.Current.MainWindow.Height);
            Process.Start(Application.ResourceAssembly.Location, args);
            Application.Current.Shutdown();
        }

        private void AppExit(object commandArg)
        {
            Application.Current.Shutdown();
        }

        private bool CanAppExit(object commandArg)
        {
            return true;
        }

        private void CanCloneChange(CanCloneChangeEvent obj)
        {
            CanClone = obj.CanClone;
        }

        private void CanCreateNewChange(CanCreateNewChangeEvent obj)
        {
            CanCreateNew = obj.CanCreate;
        }

        private void ConfirmMappingDelete(ConfirmMappingDeleteEvent obj)
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters[NavigationParameters.MappingId] = obj.MappingId.ToString();
                parameters[NavigationParameters.MappingValue] = obj.MappingValue;
                parameters[NavigationParameters.SystemName] = obj.SystemName;

                this.applicationCommands.OpenView(
                    typeof(ConfirmMappingDeleteView), 
                    RegionNames.MappingUpdateRegion, 
                    parameters);

                this.ShowUpdateMappingRegion = true;
            }
            catch (Exception)
            {
                this.ShowUpdateMappingRegion = false;
            }
        }

        private void DialogOpened(DialogOpenEvent dialogOpenEvent)
        {
            this.DialogOpen = dialogOpenEvent.DialogIsOpen;
        }

        private void HideSelectEntity(EntitySelectedEvent obj)
        {
            this.SelectEntity = false;
        }

        private void HideUpdateMapping(MappingUpdatedEvent obj)
        {
            this.ShowUpdateMappingRegion = false;

            this.applicationCommands.CloseView(typeof(MappingUpdateView), RegionNames.MappingUpdateRegion);
        }

        private void MappingDeleteConfirmed(MappingDeleteConfirmedEvent obj)
        {
            this.ShowUpdateMappingRegion = false;

            this.applicationCommands.CloseView(typeof(ConfirmMappingDeleteView), RegionNames.MappingUpdateRegion);
        }

        private void SetBusy(BusyEvent busyEvent)
        {
            this.IsBusy = busyEvent.IsBusy;
        }

        private void SetServerList()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string serverId = null;

            var items = appSettings.AllKeys.SelectMany(appSettings.GetValues, (k, v) => new { key = k, value = v });
            foreach (var item in items)
            {
                if (item.key.StartsWith("menu_service_"))
                {
                    this.ServerList.Add(item.key.Substring(13));
                }
            }

            foreach (var item in items)
            {
                if (item.value == Server.Name)
                {
                    serverId = item.key;
                    break;
                }
            }

            string serverColor = null;

            if (serverId == null)
            {
                selectedServerMenuText = Server.Name;
                this.ServerList.Add(selectedServerMenuText);
            }
            else
            {
                foreach (var item in items)
                {
                    if (item.key.StartsWith("menu_service_") && item.value == serverId)
                    {
                        selectedServerMenuText = item.key.Substring(13);
                        serverColor = ConfigurationManager.AppSettings["color_" + serverId.Substring(8)];
                        break;
                    }
                }
            }

            Color dropDownColor = (Color)ColorConverter.ConvertFromString("White");

            if (serverColor != null)
            {
                object c = ColorConverter.ConvertFromString(serverColor);
                if (c != null)
                {
                    dropDownColor = (Color)c;
                }
            }

            this.SelectedServerColor = new SolidColorBrush(dropDownColor);
        }

        private void ShowError(ErrorEvent obj)
        {
            try
            {
                this.logWriter.Write(obj.Error ?? "Unkown Exception");
            }
            catch
            {
                // A problem writing to the log should not stop the exception from being displayed    
            }

            this.Error = obj.Error ?? "Unkown Exception";

            if (obj.Exception != null)
            {
                this.logWriter.Write(obj.Exception);
                return;
            }

            if (obj.Fault != null)
            {
                this.logWriter.Write("Message: " + obj.Fault.Message + "Reason: " + obj.Fault.Reason);
            }
        }

        private void ShowSelectEntity(EntitySelectEvent obj)
        {
            try
            {
                this.applicationCommands.OpenView(
                    this.EntitySelectorViews.Where(pair => pair.Key == obj.EntityName).First().Value, 
                    obj.EntityName, 
                    obj.PropertyName, 
                    RegionNames.EntitySelectorRegion);

                this.SelectEntity = true;
            }
            catch (Exception)
            {
                this.SelectEntity = false;
            }
        }

        private void ShowUpdateMapping(MappingUpdateEvent obj)
        {
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters[NavigationParameters.EntityId] = obj.EntityId.ToString();
                parameters[NavigationParameters.MappingId] = obj.MappingId.ToString();
                parameters[NavigationParameters.MappingValue] = obj.MappingValue;
                parameters[NavigationParameters.EntityName] = obj.EntityName;

                this.applicationCommands.OpenView(
                    typeof(MappingUpdateView), 
                    RegionNames.MappingUpdateRegion, 
                    parameters);

                this.ShowUpdateMappingRegion = true;
            }
            catch (Exception)
            {
                this.ShowUpdateMappingRegion = false;
            }
        }

        private void UpdateCanSave(CanSaveEvent canSaveEvent)
        {
            this.CanSave = canSaveEvent.CanSave;
        }

        private void UpdateStatus(StatusEvent statusEvent)
        {
            this.Status = statusEvent.StatusMessage;
        }
    }
}