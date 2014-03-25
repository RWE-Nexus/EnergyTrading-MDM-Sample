﻿// This code was generated by a tool: ViewModelTemplates\EntityAddViewModelTemplate.tt
namespace Admin.LegalEntityModule.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Common.Events;
    using Common.Extensions;
    using Common.UI;

    using EnergyTrading;
    using EnergyTrading.Mdm.Client.Services;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;

    public class LegalEntityAddViewModel : NotificationObject, INavigationAware, IConfirmNavigationRequest
    {
        private readonly InteractionRequest<Confirmation> confirmationFromViewModelInteractionRequest;

        private readonly IMdmService entityService;

        private readonly IEventAggregator eventAggregator;

        private LegalEntityViewModel legalentity;

        public LegalEntityAddViewModel(
            IEventAggregator eventAggregator, 
            IMdmService entityService, 
            IList<string> partystatusConfiguration)
        {
            this.eventAggregator = eventAggregator;
            this.confirmationFromViewModelInteractionRequest = new InteractionRequest<Confirmation>();
            this.entityService = entityService;
            this.LegalEntity = new LegalEntityViewModel(this.eventAggregator);
            this.PartyStatusConfiguration = partystatusConfiguration;
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

        public IList<string> PartyStatusConfiguration { get; set; }

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

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            this.eventAggregator.Unsubscribe<SaveEvent>(this.Save);
            this.eventAggregator.Unsubscribe<EntitySelectedEvent>(this.EntitySelected);
            this.LegalEntity = new LegalEntityViewModel(this.eventAggregator);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.eventAggregator.Subscribe<SaveEvent>(this.Save);
            this.eventAggregator.Subscribe<EntitySelectedEvent>(this.EntitySelected);
        }

        public void SelectParty()
        {
            this.eventAggregator.Publish(new EntitySelectEvent("Party", "Party"));
        }

        public void StartMinimum()
        {
            this.LegalEntity.Start = DateUtility.MinDate;
        }

        public void StartToday()
        {
            this.LegalEntity.Start = SystemTime.UtcNow().Date;
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

        private void Save(SaveEvent saveEvent)
        {
            this.entityService.ExecuteAsync(
                () => this.entityService.Create(this.LegalEntity.Model()), 
                () => { this.LegalEntity = new LegalEntityViewModel(this.eventAggregator); }, 
                string.Format(Message.EntityAddedFormatString, "LegalEntity"), 
                this.eventAggregator);
        }
    }
}