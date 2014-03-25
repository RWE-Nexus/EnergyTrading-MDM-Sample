namespace Admin.ReferenceDataModule.ViewModels
{
    using System;
    using System.Linq.Expressions;
    using Common.Events;
    using Common.Extensions;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.ViewModel;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    public class ReferenceDataViewModel : NotificationObject
    {
        private readonly IEventAggregator eventAggregator;
        private readonly ReferenceData referenceData;
        private bool canSave;

        public ReferenceDataViewModel(IEventAggregator eventAggregator)
        {
        }

        public ReferenceDataViewModel(string key, string value, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.referenceData = new ReferenceData();
            this.referenceData.ReferenceKey = key;
            this.referenceData.Value = value;
            this.ReferenceKey = key;
            this.Values = value;
        }

        private System.String referenceKey;

        public System.String ReferenceKey 
        { 
            get { return this.referenceKey; }
            set { this.ChangeProperty(() => this.ReferenceKey, ref this.referenceKey, value); }
        }

        private System.String values;

        public System.String Values 
        { 
            get { return this.values; }
            set { this.ChangeProperty(() => this.Values, ref this.values, value); }
        }
            
        public bool CanSave
        {
            get
            {
                return this.canSave;
            }

            set
            {
                this.canSave = value;
                this.RaisePropertyChanged(() => this.CanSave);
            }
        }

        public ReferenceData Model()
        {
            return new ReferenceData
            {
                ReferenceKey = this.ReferenceKey,
                Value = this.Values
            };
        }

        private void ChangeProperty<T>(Expression<Func<T>> property, ref T variable, T newValue)
        {
            variable = newValue;
            this.RaisePropertyChanged(property);
            this.CanSave = this.HasChanges();
            this.eventAggregator.Publish(new CanSaveEvent(this.CanSave));
        }

        private bool HasChanges()
        {
            return
                !(
                    this.referenceData.ReferenceKey == this.ReferenceKey 
                    && this.referenceData.Value == this.Values 
                );
        }
    }
}
