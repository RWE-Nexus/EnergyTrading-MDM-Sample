namespace Common.UI.ViewModels
{
    using System;
    using Common.Events;
    using Common.Extensions;
    using Common.Services;
    using Common.UI.Uris;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;
    using EnergyTrading;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Mdm.Contracts;

    public class MappingCloneViewModel : NotificationObject, INavigationAware, IConfirmNavigationRequest
    {
        private readonly InteractionRequest<Confirmation> confirmationFromViewModelInteractionRequest;
        private readonly IMappingService mappingService;
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;
        private readonly INavigationService navigationService;
        private MappingViewModel mapping;
        private int entityId;
        private string entityName;
        private string entityInstanceName;
        private int originalEntityId;
        private int originalMappingId;

        public MappingCloneViewModel(
            IEventAggregator eventAggregator,
            IMappingService mappingService,
            IRegionManager regionManager,
            INavigationService navigationService)
        {
            this.eventAggregator = eventAggregator;
            this.mappingService = mappingService;
            this.regionManager = regionManager;
            this.navigationService = navigationService;
            this.confirmationFromViewModelInteractionRequest = new InteractionRequest<Confirmation>();
        }

        /// <summary>
        /// Gets the notification from view model interaction request. View binds to this property
        /// </summary>
        public IInteractionRequest ConfirmationFromViewModelInteractionRequest
        {
            get
            {
                return this.confirmationFromViewModelInteractionRequest;
            }
        }

        public MappingViewModel Mapping
        {
            get
            {
                return this.mapping;
            }

            set
            {
                this.mapping = value;
                this.RaisePropertyChanged(() => this.Mapping);
            }
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (this.Mapping.CanSave)
            {
                this.confirmationFromViewModelInteractionRequest.Raise(
                    new Confirmation { Content = Message.UnsavedChanges, Title = Message.UnsavedChangeTitle },
                    confirmation => continuationCallback(confirmation.Confirmed));
            }
            else
            {
                continuationCallback(true);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            this.eventAggregator.Unsubscribe<SaveEvent>(this.Save);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.eventAggregator.Subscribe<SaveEvent>(this.Save);

            this.originalMappingId = int.Parse(navigationContext.Parameters[NavigationParameters.MappingId]);
            this.entityId = int.Parse(navigationContext.Parameters[NavigationParameters.EntityId]);
            this.originalEntityId = int.Parse(navigationContext.Parameters[NavigationParameters.OriginalEntityId]);
            this.entityName = navigationContext.Parameters[NavigationParameters.EntityName];
            this.entityInstanceName = navigationContext.Parameters[NavigationParameters.EntityInstanceName];
            RaisePropertyChanged("Context");

            this.LoadMappingFromService(this.originalEntityId, this.originalMappingId, this.entityName);
        }

        public void StartToday()
        {
            this.Mapping.StartDate = SystemTime.UtcNow().Date;
        }

        public void StartMinimum()
        {
            this.Mapping.StartDate = DateUtility.MinDate;
        }

        private void LoadMappingFromService(int pid, int mappingId, string entityName)
        {
            EntityWithETag<MdmId> mappingFromService = this.mappingService.GetMapping(entityName, pid, mappingId);

            this.Mapping = new MappingViewModel(mappingFromService, this.eventAggregator);

            // clear down the mapping id
            this.Mapping.MappingId = null;
            this.RaisePropertyChanged(string.Empty);
        }

        private void CreateMapping(CreateEvent obj)
        {
            this.regionManager.RequestNavigate(RegionNames.MainRegion, ViewNames.MappingAddView);
        }

        private void Save(SaveEvent saveEvent)
        {
            WebResponse<MdmId> response = this.mappingService.CreateMapping(this.entityName, this.entityId, this.Mapping.Model());

            if (response.IsValid)
            {
                this.eventAggregator.Publish(new StatusEvent(Message.MappingAdded));
                this.eventAggregator.Publish(new MappingClonedEvent(this.originalEntityId, this.originalMappingId));
                this.Mapping = new MappingViewModel(this.eventAggregator);
                this.navigationService.NavigateMainBackWithStatus(new StatusEvent(Message.MappingAdded));
                return;
            }

            this.eventAggregator.Publish(new ErrorEvent(response.Fault != null ? response.Fault.Message : "Unkown Error"));
        }

        public string Context
        {
            get
            {
                return this.entityName + ": " + this.entityInstanceName;
            }
        }
    }
}