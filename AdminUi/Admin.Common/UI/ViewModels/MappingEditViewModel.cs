namespace Common.UI.ViewModels
{
    using System;
    using System.Net;

    using Common.Events;
    using Common.Extensions;
    using Common.Services;
    using Common.UI.Uris;

    using EnergyTrading;
    using EnergyTrading.Mdm.Contracts;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;

    public class MappingEditViewModel : NotificationObject, INavigationAware, IConfirmNavigationRequest
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

        public MappingEditViewModel(
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
            this.eventAggregator.Unsubscribe<CreateEvent>(this.CreateMapping);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.eventAggregator.Subscribe<SaveEvent>(this.Save);
            this.eventAggregator.Subscribe<CreateEvent>(this.CreateMapping);

            int mappingId = int.Parse(navigationContext.Parameters[NavigationParameters.MappingId]);
            this.entityId = int.Parse(navigationContext.Parameters[NavigationParameters.EntityId]);
            this.entityName = navigationContext.Parameters[NavigationParameters.EntityName];
            this.entityInstanceName = navigationContext.Parameters[NavigationParameters.EntityInstanceName];
            RaisePropertyChanged("Context");

            this.LoadMappingFromService(this.entityId, mappingId, this.entityName);
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
            this.RaisePropertyChanged(string.Empty);
        }

        private void CreateMapping(CreateEvent obj)
        {
            this.regionManager.RequestNavigate(RegionNames.MainRegion, ViewNames.MappingAddView);
        }

        private void Save(SaveEvent saveEvent)
        {
            if (this.ShouldDeleteAndRecreate())
            {
                var errorMessage = this.DeleteAndRecreate();

                if (errorMessage == null)
                {
                    this.Mapping = new MappingViewModel(this.eventAggregator);
                    this.navigationService.NavigateMainBackWithStatus(new StatusEvent(Message.MappingUpdated));
                    return;
                }

                this.eventAggregator.Publish(new ErrorEvent(string.Format(Message.MappingUpdateFailed, errorMessage)));
                return;
            }

            var response = this.mappingService.UpdateMapping(
                this.entityName,
                this.Mapping.MappingId.Value,
                this.entityId,
                new EntityWithETag<MdmId>(this.Mapping.Model(), this.Mapping.ETag));

            if (response.IsValid)
            {
                this.Mapping = new MappingViewModel(this.eventAggregator);
                this.navigationService.NavigateMainBackWithStatus(new StatusEvent(Message.MappingUpdated));
                return;
            }

            this.eventAggregator.Publish(new ErrorEvent(response.Fault != null ? response.Fault.Message : "Unkown Error"));
        }

        private string DeleteAndRecreate()
        {
            string error = null;
            this.mappingService.DeleteMapping(this.entityName, this.Mapping.MappingId.Value, this.entityId);

            var mappingWithIdStripped = this.Mapping.Model();
            mappingWithIdStripped.MappingId = null;

            var response = this.mappingService.CreateMapping(this.entityName, this.entityId, mappingWithIdStripped);

            if (response.Code != HttpStatusCode.Created)
            {
                var newMapping = this.mappingService.CreateMapping(this.entityName, this.entityId, this.Mapping.OriginalModel());
                var returnUriParts = newMapping.Location.Split('/');
                this.LoadMappingFromService(
                    this.entityId, Convert.ToInt32(returnUriParts[returnUriParts.Length - 1]), this.entityName);
                error = response.Fault != null ? response.Fault.Message : "Unkown Error";
            }

            return error;
        }

        private bool ShouldDeleteAndRecreate()
        {
            return this.Mapping.MappingStringChanged || this.Mapping.DefaultReverseIndChanged;
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