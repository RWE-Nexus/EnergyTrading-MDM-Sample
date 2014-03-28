namespace Admin.ReferenceDataModule.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using Common.Events;
    using Common.Extensions;

    using EnergyTrading.Mdm.Contracts;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.ViewModel;

    public class ReferenceDataViewModel : NotificationObject
    {
        private readonly IEventAggregator eventAggregator;

        private readonly ReferenceData referenceData;

        private bool canSave;

        private string referenceKey;

        private string values;

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

        public string ReferenceKey
        {
            get
            {
                return this.referenceKey;
            }

            set
            {
                this.ChangeProperty(() => this.ReferenceKey, ref this.referenceKey, value);
            }
        }

        public string Values
        {
            get
            {
                return this.values;
            }

            set
            {
                this.ChangeProperty(() => this.Values, ref this.values, value);
            }
        }

        public ReferenceData Model()
        {
            return new ReferenceData { ReferenceKey = this.ReferenceKey, Value = this.Values };
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
            return !(this.referenceData.ReferenceKey == this.ReferenceKey && this.referenceData.Value == this.Values);
        }
    }
}