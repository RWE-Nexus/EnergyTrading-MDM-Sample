﻿// This code was generated by a tool: ViewModelTemplates\EntityAddViewModelTemplate.tt
namespace Admin.PersonModule.ViewModels
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

    public class PersonAddViewModel : NotificationObject, INavigationAware, IConfirmNavigationRequest
    {
        private readonly InteractionRequest<Confirmation> confirmationFromViewModelInteractionRequest;

        private readonly IMdmService entityService;

        private readonly IEventAggregator eventAggregator;

        private PersonViewModel person;

        public PersonAddViewModel(
            IEventAggregator eventAggregator, 
            IMdmService entityService, 
            IList<string> roleConfiguration)
        {
            this.eventAggregator = eventAggregator;
            this.confirmationFromViewModelInteractionRequest = new InteractionRequest<Confirmation>();
            this.entityService = entityService;

            this.Person = new PersonViewModel(this.eventAggregator);
            this.RoleConfiguration = roleConfiguration;
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

        public PersonViewModel Person
        {
            get
            {
                return this.person;
            }

            set
            {
                this.person = value;
                this.RaisePropertyChanged(() => this.Person);
            }
        }

        public IList<string> RoleConfiguration { get; set; }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (this.Person.CanSave)
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

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            this.eventAggregator.Unsubscribe<SaveEvent>(this.Save);
            this.eventAggregator.Unsubscribe<EntitySelectedEvent>(this.EntitySelected);
            this.Person = new PersonViewModel(this.eventAggregator);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.eventAggregator.Subscribe<SaveEvent>(this.Save);
            this.eventAggregator.Subscribe<EntitySelectedEvent>(this.EntitySelected);
        }

        public void StartMinimum()
        {
            this.Person.Start = DateUtility.MinDate;
        }

        public void StartToday()
        {
            this.Person.Start = SystemTime.UtcNow().Date;
        }

        private void EntitySelected(EntitySelectedEvent obj)
        {
        }

        private void Save(SaveEvent saveEvent)
        {
            this.entityService.ExecuteAsync(
                () => this.entityService.Create(this.Person.Model()), 
                () => { this.Person = new PersonViewModel(this.eventAggregator); }, 
                string.Format(Message.EntityAddedFormatString, "Person"), 
                this.eventAggregator);
        }
    }
}