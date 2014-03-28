namespace Admin.ReferenceDataModule.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using Common.Events;
    using Common.Extensions;
    using Common.UI;

    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.Mdm.Contracts;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;

    public class ReferenceDataAddViewModel : NotificationObject, INavigationAware, IConfirmNavigationRequest
    {
        private readonly InteractionRequest<Confirmation> confirmationFromViewModelInteractionRequest;

        private readonly IReferenceDataService entityService;

        private readonly IEventAggregator eventAggregator;

        private ReferenceDataViewModel referenceData;

        public ReferenceDataAddViewModel(IEventAggregator eventAggregator, IReferenceDataService entityService)
        {
            this.eventAggregator = eventAggregator;
            this.confirmationFromViewModelInteractionRequest = new InteractionRequest<Confirmation>();
            this.entityService = entityService;
            this.ReferenceData = new ReferenceDataViewModel(null, null, this.eventAggregator);
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
            this.ReferenceData = new ReferenceDataViewModel(null, null, this.eventAggregator);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.eventAggregator.Subscribe<SaveEvent>(this.Save);
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
                    () => this.ReferenceData = new ReferenceDataViewModel(this.eventAggregator), 
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