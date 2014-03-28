namespace Admin.ReferenceDataModule.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using Common;
    using Common.Authorisation;
    using Common.Events;
    using Common.Extensions;
    using Common.Services;
    using Common.UI;

    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.Mdm.Contracts;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;

    public class ReferenceDataEditViewModel : NotificationObject, INavigationAware, IConfirmNavigationRequest
    {
        private readonly IApplicationCommands applicationCommands;

        private readonly InteractionRequest<Confirmation> confirmationFromViewModelInteractionRequest;

        private readonly IReferenceDataService entityService;

        private readonly IEventAggregator eventAggregator;

        private readonly IMappingService mappingService;

        private readonly INavigationService navigationService;

        private ReferenceDataViewModel referenceData;

        public ReferenceDataEditViewModel(
            IEventAggregator eventAggregator, 
            IReferenceDataService entityService, 
            INavigationService navigationService, 
            IMappingService mappingService, 
            IApplicationCommands applicationCommands)
        {
            this.navigationService = navigationService;
            this.mappingService = mappingService;
            this.applicationCommands = applicationCommands;
            this.eventAggregator = eventAggregator;
            this.entityService = entityService;
            this.confirmationFromViewModelInteractionRequest = new InteractionRequest<Confirmation>();
            this.CanEdit = AuthorisationHelpers.HasEntityRights("ReferenceData");
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

        public ReferenceDataViewModel ReferenceData
        {
            get
            {
                return this.referenceData;
            }

            set
            {
                this.referenceData = value;
                this.RaisePropertyChanged(() => this.ReferenceData);
            }
        }

        public string Values
        {
            get
            {
                try
                {
                    return this.referenceData.Values.Replace('|', '\n');
                }
                catch (Exception)
                {
                    return null;
                }
            }

            set
            {
                this.referenceData.Values = value;
            }
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (this.ReferenceData.CanSave)
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
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.eventAggregator.Subscribe<SaveEvent>(this.Save);
            string idParam = navigationContext.Parameters[NavigationParameters.EntityId];
            string valueParam = navigationContext.Parameters[NavigationParameters.EntityValue];

            this.LoadReferenceDataFromService(idParam, valueParam);
        }

        private void LoadReferenceDataFromService(string referenceDataId, string referenceDataValue)
        {
            this.ReferenceData = new ReferenceDataViewModel(referenceDataId, referenceDataValue, this.eventAggregator);

            this.RaisePropertyChanged(string.Empty);
        }

        private void Save(SaveEvent saveEvent)
        {
            try
            {
                var rds = this.referenceData.Values.Split('\n');
                List<ReferenceData> listReferenceData = new List<ReferenceData>();

                foreach (var rd in rds)
                {
                    listReferenceData.Add(
                        new ReferenceData { ReferenceKey = this.ReferenceData.ReferenceKey, Value = rd });
                }

                this.entityService.ExecuteAsyncRD(
                    () => this.entityService.Create(this.ReferenceData.ReferenceKey, listReferenceData), 
                    () => this.LoadReferenceDataFromService(this.ReferenceData.ReferenceKey, this.ReferenceData.Values), 
                    string.Format(Message.EntityUpdatedFormatString, "ReferenceData"), 
                    this.eventAggregator);
            }
            catch (Exception)
            {
                MessageBox.Show("No values supplied", Application.Current.MainWindow.Title);
            }
        }
    }
}