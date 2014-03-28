namespace Common.UI.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common.Authorisation;
    using Common.Events;
    using Common.Extensions;
    using Common.Services;

    using EnergyTrading;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Mdm.Contracts;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;

    public class MappingAddViewModel : NotificationObject, IConfirmNavigationRequest
    {
        private readonly InteractionRequest<Confirmation> confirmationFromViewModelInteractionRequest;

        private readonly IEventAggregator eventAggregator;

        private readonly IMappingService mappingService;

        private readonly INavigationService navigationService;

        private string entityId;

        private string entityInstanceName;

        private string entityName;

        private MappingViewModel mapping;

        private IList<string> sourceSystems;

        public MappingAddViewModel(
            IEventAggregator eventAggregator, 
            IMappingService mappingService, 
            INavigationService navigationService)
        {
            this.eventAggregator = eventAggregator;
            this.mappingService = mappingService;
            this.navigationService = navigationService;
            this.confirmationFromViewModelInteractionRequest = new InteractionRequest<Confirmation>();

            this.SourceSystems = mappingService.GetSourceSystemNames().OrderBy(x => x).ToList();
            this.SourceSystems.Insert(0, string.Empty);
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

        public string Context
        {
            get
            {
                return this.entityName + ": " + this.entityInstanceName;
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
            this.Mapping = new MappingViewModel(this.eventAggregator);

            this.entityId = navigationContext.Parameters[NavigationParameters.EntityId];
            this.entityName = navigationContext.Parameters[NavigationParameters.EntityName];
            this.entityInstanceName = navigationContext.Parameters[NavigationParameters.EntityInstanceName];
            this.eventAggregator.Subscribe<SaveEvent>(this.Save);
            this.SourceSystems =
                mappingService.GetSourceSystemNames()
                    .Where(x => AuthorisationHelpers.HasMappingRights(this.entityName, x))
                    .OrderBy(x => x)
                    .ToList();
            this.SourceSystems.Insert(0, string.Empty);

            RaisePropertyChanged("Context");
        }

        public void StartMinimum()
        {
            this.Mapping.StartDate = DateUtility.MinDate;
        }

        public void StartToday()
        {
            this.Mapping.StartDate = SystemTime.UtcNow().Date;
        }

        private void Save(SaveEvent saveEvent)
        {
            WebResponse<MdmId> response = this.mappingService.CreateMapping(
                this.entityName, 
                int.Parse(this.entityId), 
                this.Mapping.Model());

            if (response.IsValid)
            {
                this.eventAggregator.Publish(new StatusEvent(Message.MappingAdded));
                this.Mapping = new MappingViewModel(this.eventAggregator);
                this.navigationService.NavigateMainBackWithStatus(new StatusEvent(Message.MappingAdded));
                return;
            }

            this.eventAggregator.Publish(
                new ErrorEvent(response.Fault != null ? response.Fault.Message : "Unknown Error"));
        }
    }
}