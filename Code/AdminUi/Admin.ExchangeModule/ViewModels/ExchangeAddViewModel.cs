﻿// This code was generated by a tool: ViewModelTemplates\EntityAddViewModelTemplate.tt
namespace Admin.ExchangeModule.ViewModels
{
    using System;

    using Common.Events;
    using Common.Extensions;
    using Common.UI;

    using EnergyTrading;
    using EnergyTrading.Mdm.Client.Services;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;

    public class ExchangeAddViewModel : NotificationObject, INavigationAware, IConfirmNavigationRequest
    {
        private readonly InteractionRequest<Confirmation> confirmationFromViewModelInteractionRequest;

        private readonly IMdmService entityService;

        private readonly IEventAggregator eventAggregator;

        private ExchangeViewModel exchange;

        public ExchangeAddViewModel(IEventAggregator eventAggregator, IMdmService entityService)
        {
            this.eventAggregator = eventAggregator;
            this.confirmationFromViewModelInteractionRequest = new InteractionRequest<Confirmation>();
            this.entityService = entityService;
            this.Exchange = new ExchangeViewModel(this.eventAggregator);
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

        public ExchangeViewModel Exchange
        {
            get
            {
                return this.exchange;
            }

            set
            {
                this.exchange = value;
                this.RaisePropertyChanged(() => this.Exchange);
            }
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (this.Exchange.CanSave)
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
            this.Exchange.PartyId = null;
            this.Exchange.PartyName = string.Empty;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            this.eventAggregator.Unsubscribe<SaveEvent>(this.Save);
            this.eventAggregator.Unsubscribe<EntitySelectedEvent>(this.EntitySelected);
            this.Exchange = new ExchangeViewModel(this.eventAggregator);
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
            this.Exchange.Start = DateUtility.MinDate;
        }

        public void StartToday()
        {
            this.Exchange.Start = SystemTime.UtcNow().Date;
        }

        private void EntitySelected(EntitySelectedEvent obj)
        {
            switch (obj.EntityKey)
            {
                case "Party":
                    this.Exchange.PartyId = obj.Id;
                    this.Exchange.PartyName = obj.EntityValue;
                    break;
            }
        }

        private void Save(SaveEvent saveEvent)
        {
            this.entityService.ExecuteAsync(
                () => this.entityService.Create(this.Exchange.Model()), 
                () => { this.Exchange = new ExchangeViewModel(this.eventAggregator); }, 
                string.Format(Message.EntityAddedFormatString, "Exchange"), 
                this.eventAggregator);
        }
    }
}