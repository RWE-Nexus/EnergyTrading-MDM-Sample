﻿// This code was generated by a tool: ViewModelTemplates\EntityAddViewModelTemplate.tt
namespace Admin.SourceSystemModule.ViewModels
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

    public class SourceSystemAddViewModel : NotificationObject, INavigationAware, IConfirmNavigationRequest
    {
        private readonly InteractionRequest<Confirmation> confirmationFromViewModelInteractionRequest;

        private readonly IMdmService entityService;

        private readonly IEventAggregator eventAggregator;

        private SourceSystemViewModel sourcesystem;

        public SourceSystemAddViewModel(IEventAggregator eventAggregator, IMdmService entityService)
        {
            this.eventAggregator = eventAggregator;
            this.confirmationFromViewModelInteractionRequest = new InteractionRequest<Confirmation>();
            this.entityService = entityService;

            this.SourceSystem = new SourceSystemViewModel(this.eventAggregator);
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

        public SourceSystemViewModel SourceSystem
        {
            get
            {
                return this.sourcesystem;
            }

            set
            {
                this.sourcesystem = value;
                this.RaisePropertyChanged(() => this.SourceSystem);
            }
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (this.SourceSystem.CanSave)
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

        public void DeleteParent()
        {
            this.SourceSystem.ParentId = null;
            this.SourceSystem.ParentName = string.Empty;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            this.eventAggregator.Unsubscribe<SaveEvent>(this.Save);
            this.eventAggregator.Unsubscribe<EntitySelectedEvent>(this.EntitySelected);
            this.SourceSystem = new SourceSystemViewModel(this.eventAggregator);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.eventAggregator.Subscribe<SaveEvent>(this.Save);
            this.eventAggregator.Subscribe<EntitySelectedEvent>(this.EntitySelected);
        }

        public void SelectParent()
        {
            this.eventAggregator.Publish(new EntitySelectEvent("SourceSystem", "Parent"));
        }

        public void StartMinimum()
        {
            this.SourceSystem.Start = DateUtility.MinDate;
        }

        public void StartToday()
        {
            this.SourceSystem.Start = SystemTime.UtcNow().Date;
        }

        private void EntitySelected(EntitySelectedEvent obj)
        {
            switch (obj.EntityKey)
            {
                case "Parent":
                    this.SourceSystem.ParentId = obj.Id;
                    this.SourceSystem.ParentName = obj.EntityValue;
                    break;
            }
        }

        private void Save(SaveEvent saveEvent)
        {
            this.entityService.ExecuteAsync(
                () => this.entityService.Create(this.SourceSystem.Model()), 
                () => { this.SourceSystem = new SourceSystemViewModel(this.eventAggregator); }, 
                string.Format(Message.EntityAddedFormatString, "SourceSystem"), 
                this.eventAggregator);
        }
    }
}