namespace Admin.LegalEntityModule.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Common;
    using Common.Authorisation;
    using Common.Commands;
    using Common.Events;
    using Common.Extensions;
    using Common.Services;
    using Common.UI;
    using Common.UI.Uris;
    using Common.UI.ViewModels;

    using EnergyTrading;
    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Sample;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;

    public class LegalEntityEditViewModel : NotificationObject, IConfirmNavigationRequest
    {
        private readonly IApplicationCommands applicationCommands;

        private readonly InteractionRequest<Confirmation> confirmationFromViewModelInteractionRequest;

        private readonly IMdmService entityService;

        private readonly IEventAggregator eventAggregator;

        private readonly IMappingService mappingService;

        private readonly INavigationService navigationService;

        private ICommand deleteMappingCommand;

        private LegalEntityViewModel legalentity;

        private ObservableCollection<MappingViewModel> mappings;

        private MappingViewModel selectedMapping;

        private ICommand updateMappingCommand;

        private DateTime validAtString;

        public LegalEntityEditViewModel(
            IEventAggregator eventAggregator, 
            IMdmService entityService, 
            INavigationService navigationService, 
            IMappingService mappingService, 
            IApplicationCommands applicationCommands, 
            IList<string> partystatusConfiguration)
        {
            this.navigationService = navigationService;
            this.mappingService = mappingService;
            this.applicationCommands = applicationCommands;
            this.eventAggregator = eventAggregator;
            this.entityService = entityService;
            this.confirmationFromViewModelInteractionRequest = new InteractionRequest<Confirmation>();
            this.CanEdit = AuthorisationHelpers.HasEntityRights("LegalEntity");

            this.PartyStatusConfiguration = partystatusConfiguration;
        }

        public bool CanEdit { get; private set; }

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

        public ICommand DeleteMappingCommand
        {
            get
            {
                if (this.deleteMappingCommand == null)
                {
                    this.deleteMappingCommand = new RelayCommand(
                        param => this.DeleteMapping(param), 
                        param => CanEditOrDeleteMapping(param));
                }

                return this.deleteMappingCommand;
            }
        }

        public LegalEntityViewModel LegalEntity
        {
            get
            {
                return this.legalentity;
            }

            set
            {
                this.legalentity = value;
                this.RaisePropertyChanged(() => this.LegalEntity);
            }
        }

        public ObservableCollection<MappingViewModel> Mappings
        {
            get
            {
                return this.mappings;
            }

            set
            {
                this.mappings = value;
                this.RaisePropertyChanged(() => this.Mappings);
            }
        }

        public IList<string> PartyStatusConfiguration { get; set; }

        public MappingViewModel SelectedMapping
        {
            get
            {
                return this.selectedMapping;
            }

            set
            {
                this.selectedMapping = value;
                this.RaisePropertyChanged(() => this.SelectedMapping);
            }
        }

        public ICommand UpdateMappingCommand
        {
            get
            {
                if (this.updateMappingCommand == null)
                {
                    this.updateMappingCommand = new RelayCommand(
                        param => this.UpdateMapping(param), 
                        param => CanEditOrDeleteMapping(param));
                }

                return this.updateMappingCommand;
            }
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (this.LegalEntity.CanSave)
            {
                this.eventAggregator.Publish(new DialogOpenEvent(true));
                this.confirmationFromViewModelInteractionRequest.Raise(
                    new Confirmation { Content = Message.UnsavedChanges, Title = Message.UnsavedChangeTitle }, 
                    confirmation =>
                        {
                            continuationCallback(confirmation.Confirmed);
                            this.eventAggregator.Publish(new DialogOpenEvent(false));
                        });
            }
            else
            {
                continuationCallback(true);
            }
        }

        public void DeleteParty()
        {
            this.LegalEntity.PartyId = null;
            this.LegalEntity.PartyName = string.Empty;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void NavigateToDetail(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.NavigateToDetailScreen();
            }
        }

        public void NavigateToDetailDoubleClick()
        {
            this.NavigateToDetailScreen();
        }

        public void NavigateToParty()
        {
            this.navigationService.NavigateMain(
                new EntityEditUri("Party", this.LegalEntity.PartyId, this.LegalEntity.Start));
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            this.eventAggregator.Unsubscribe<SaveEvent>(this.Save);
            this.eventAggregator.Unsubscribe<CreateEvent>(this.CreateMapping);
            this.eventAggregator.Unsubscribe<EntitySelectedEvent>(this.EntitySelected);
            this.eventAggregator.Unsubscribe<MappingUpdatedEvent>(this.MappingUpdated);
            this.eventAggregator.Unsubscribe<MappingDeleteConfirmedEvent>(this.MappingDeleteConfirmed);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.eventAggregator.Subscribe<SaveEvent>(this.Save);
            this.eventAggregator.Subscribe<CreateEvent>(this.CreateMapping);
            this.eventAggregator.Subscribe<MappingUpdatedEvent>(this.MappingUpdated);
            this.eventAggregator.Subscribe<MappingDeleteConfirmedEvent>(this.MappingDeleteConfirmed);
            this.eventAggregator.Subscribe<EntitySelectedEvent>(this.EntitySelected);
            int idParam = int.Parse(navigationContext.Parameters[NavigationParameters.EntityId]);
            DateTime validAtStringParam = DateTime.Parse(navigationContext.Parameters[NavigationParameters.ValidAtDate]);

            if (this.LegalEntity == null || this.validAtString != validAtStringParam || this.LegalEntity.Id != idParam)
            {
            }

            this.validAtString = validAtStringParam;
            this.LoadLegalEntityFromService(idParam, validAtString);

            this.eventAggregator.Publish(new CanCreateNewChangeEvent(CanAddMappings()));
        }

        public void SelectParty()
        {
            this.eventAggregator.Publish(new EntitySelectEvent("Party", "Party"));
        }

        public void Sorting()
        {
            this.SelectedMapping = null;
        }

        public void StartMinimum()
        {
            this.LegalEntity.Start = DateUtility.MinDate;
        }

        public void StartToday()
        {
            this.LegalEntity.Start = SystemTime.UtcNow().Date;
        }

        private static MdmId NewMapping(EntityWithETag<MdmId> entityWithETag, MappingUpdatedEvent updatedEvent)
        {
            return new MdmId
                       {
                           DefaultReverseInd = updatedEvent.IsDefault, 
                           IsMdmId = false, 
                           SourceSystemOriginated = updatedEvent.IsSourceSystemOriginated, 
                           StartDate = updatedEvent.StartDate, 
                           Identifier = updatedEvent.NewValue, 
                           EndDate = entityWithETag.Object.EndDate, 
                           SystemName = entityWithETag.Object.SystemName, 
                       };
        }

        private bool CanAddMappings()
        {
            foreach (var system in mappingService.GetSourceSystemNames())
            {
                if (AuthorisationHelpers.HasMappingRights("LegalEntity", system))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CanEditOrDeleteMapping(object mapping)
        {
            var mappingViewModel = mapping as MappingViewModel;
            if (mappingViewModel == null)
            {
                return false;
            }

            if (mappingViewModel.MappingId == null)
            {
                return false;
            }

            return CanEditOrDeleteMapping(mappingViewModel.MappingId.Value);
        }

        private bool CanEditOrDeleteMapping(int mappingId)
        {
            var system = Mappings.Where(x => x.MappingId == mappingId).Select(x => x.SystemName).FirstOrDefault();
            return AuthorisationHelpers.HasMappingRights("LegalEntity", system);
        }

        private void CreateMapping(CreateEvent obj)
        {
            this.navigationService.NavigateMain(
                new MappingAddUri(this.LegalEntity.Id.Value, "LegalEntity", this.LegalEntity.Name));
        }

        private void DeleteMapping(object mapping)
        {
            var mappingViewModel = mapping as MappingViewModel;
            if (mappingViewModel == null)
            {
                return;
            }

            if (mappingViewModel.MappingId == null)
            {
                return;
            }

            this.eventAggregator.Publish(
                new ConfirmMappingDeleteEvent(
                    mappingViewModel.MappingId.Value, 
                    mappingViewModel.MappingString, 
                    mappingViewModel.SystemName));
        }

        private void EntitySelected(EntitySelectedEvent obj)
        {
            switch (obj.EntityKey)
            {
                case "Party":
                    this.LegalEntity.PartyId = obj.Id;
                    this.LegalEntity.PartyName = obj.EntityValue;
                    break;
            }
        }

        private void LoadLegalEntityFromService(
            int legalentityId, 
            DateTime validAt, 
            bool publishChangeNotification = false)
        {
            this.entityService.ExecuteAsync(
                () => this.entityService.Get<LegalEntity>(legalentityId, validAt), 
                response =>
                    {
                        this.LegalEntity =
                            new LegalEntityViewModel(
                                new EntityWithETag<LegalEntity>(response.Message, response.Tag), 
                                this.eventAggregator);

                        this.Mappings =
                            new ObservableCollection<MappingViewModel>(
                                response.Message.Identifiers.Select(
                                    nexusId =>
                                    new MappingViewModel(new EntityWithETag<MdmId>(nexusId, null), this.eventAggregator)));

                        if (publishChangeNotification)
                        {
                        }

                        this.RaisePropertyChanged(string.Empty);
                    }, 
                this.eventAggregator);
        }

        private void MappingDeleteConfirmed(MappingDeleteConfirmedEvent obj)
        {
            if (obj.Cancelled)
            {
                return;
            }

            var response = this.mappingService.DeleteMapping("LegalEntity", obj.MappingId, this.LegalEntity.Id.Value);

            if (response.IsValid)
            {
                this.LoadLegalEntityFromService(this.legalentity.Id.Value, validAtString, true);
                this.eventAggregator.Publish(new StatusEvent(Message.MappingDeleted));
                return;
            }

            this.eventAggregator.Publish(
                new ErrorEvent(response.Fault != null ? response.Fault.Message : "Unknown Error"));
        }

        private void MappingUpdated(MappingUpdatedEvent updatedEvent)
        {
            if (updatedEvent.Cancelled)
            {
                return;
            }

            EntityWithETag<MdmId> entityWithETag;
            if (!TryGetMapping("LegalEntity", updatedEvent, out entityWithETag))
            {
                return;
            }

            if (entityWithETag.Object.EndDate <= updatedEvent.StartDate)
            {
                var message = string.Format(
                    "The start date of the new mapping must be before {0}", 
                    entityWithETag.Object.EndDate);
                this.eventAggregator.Publish(new ErrorEvent(message));
                return;
            }

            var newMapping = NewMapping(entityWithETag, updatedEvent);

            if (!TryCreateMapping("LegalEntity", newMapping, updatedEvent))
            {
                return;
            }

            if (!TryGetMapping("LegalEntity", updatedEvent, out entityWithETag))
            {
                return;
            }

            entityWithETag.Object.EndDate = updatedEvent.StartDate.AddSeconds(-1);

            if (!TryUpdateMapping("LegalEntity", entityWithETag, updatedEvent))
            {
                return;
            }

            this.LoadLegalEntityFromService(updatedEvent.EntityId, updatedEvent.StartDate, true);
            this.eventAggregator.Publish(new StatusEvent(Message.MappingUpdated));
        }

        private void NavigateToDetailScreen()
        {
            if (this.SelectedMapping != null && CanEditOrDeleteMapping(this.SelectedMapping))
            {
                if (!this.SelectedMapping.IsMdmId)
                {
                    this.navigationService.NavigateMain(
                        new MappingEditUri(
                            this.LegalEntity.Id.Value, 
                            "LegalEntity", 
                            Convert.ToInt32(this.SelectedMapping.MappingId), 
                            this.LegalEntity.Name));
                    return;
                }

                this.eventAggregator.Publish(new StatusEvent("MdmSystemData ID cannot be edited"));
            }
        }

        private void Save(SaveEvent saveEvent)
        {
            this.entityService.ExecuteAsync(
                () =>
                this.entityService.Update(this.LegalEntity.Id.Value, this.LegalEntity.Model(), this.LegalEntity.ETag), 
                () => this.LoadLegalEntityFromService(this.LegalEntity.Id.Value, this.LegalEntity.Start, true), 
                string.Format(Message.EntityUpdatedFormatString, "LegalEntity"), 
                this.eventAggregator);
        }

        private bool TryCreateMapping(string entityName, MdmId newMapping, MappingUpdatedEvent updatedEvent)
        {
            var response = mappingService.CreateMapping(entityName, updatedEvent.EntityId, newMapping);
            if (!response.IsValid)
            {
                this.eventAggregator.Publish(
                    new ErrorEvent(response.Fault != null ? response.Fault.Message : "Unknown Error"));
                return false;
            }

            return true;
        }

        private bool TryGetMapping(
            string entityName, 
            MappingUpdatedEvent updatedEvent, 
            out EntityWithETag<MdmId> mapping)
        {
            mapping = mappingService.GetMapping(entityName, updatedEvent.EntityId, updatedEvent.MappingId);
            if (mapping.Object == null)
            {
                this.eventAggregator.Publish(new ErrorEvent("Unable to retrieve original mapping"));
                return false;
            }

            return true;
        }

        private bool TryUpdateMapping(
            string entityName, 
            EntityWithETag<MdmId> entityWithETag, 
            MappingUpdatedEvent updatedEvent)
        {
            var response = mappingService.UpdateMapping(
                entityName, 
                updatedEvent.MappingId, 
                updatedEvent.EntityId, 
                entityWithETag);
            if (!response.IsValid)
            {
                this.eventAggregator.Publish(
                    new ErrorEvent(response.Fault != null ? response.Fault.Message : "Unknown Error"));
                return false;
            }

            return true;
        }

        private void UpdateMapping(object mapping)
        {
            var mappingViewModel = mapping as MappingViewModel;
            if (mappingViewModel == null)
            {
                return;
            }

            if (mappingViewModel.MappingId == null)
            {
                return;
            }

            if (mappingViewModel != null)
            {
                this.eventAggregator.Publish(
                    new MappingUpdateEvent(
                        this.LegalEntity.Id.Value, 
                        mappingViewModel.MappingId.Value, 
                        mappingViewModel.MappingString, 
                        "LegalEntity"));
            }
        }
    }
}