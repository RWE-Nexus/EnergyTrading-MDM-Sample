namespace Common.UI.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using Common.Events;
    using Common.Extensions;
    using Common.Services;

    using EnergyTrading;
    using EnergyTrading.Mdm.Contracts;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.ViewModel;

    public class MappingViewModel : NotificationObject
    {
        private readonly IEventAggregator eventAggregator;

        private readonly MdmId nexusId;

        private bool defaultReverseInd;

        private DateTime endDate;

        private bool identifier;

        private bool isMdmId;

        private int? mappingId;

        private string mappingString;

        private bool sourceSystemOriginated;

        private DateTime startDate;

        private string systemName;

        public MappingViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.nexusId = this.NewMdmId();

            this.SystemName = this.nexusId.SystemName;
            this.MappingString = this.nexusId.Identifier;
            this.SourceSystemOriginated = this.nexusId.SourceSystemOriginated;
            this.DefaultReverseInd = this.nexusId.DefaultReverseInd.Value;
            this.StartDate = this.nexusId.StartDate.Value;
            this.EndDate = this.nexusId.EndDate.Value;
        }

        public MappingViewModel(EntityWithETag<MdmId> ewe, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.nexusId = ewe.Object;

            // TODO: probably need to change this to not be nullable
            if (this.nexusId.DefaultReverseInd == null)
            {
                this.nexusId.DefaultReverseInd = false;
            }

            this.ETag = ewe.ETag;
            this.DefaultReverseInd = this.nexusId.DefaultReverseInd.Value;
            this.IsMdmId = this.nexusId.IsMdmId;
            this.EndDate = this.nexusId.EndDate.Value;
            this.StartDate = this.nexusId.StartDate.Value;
            this.MappingId = (int?)this.nexusId.MappingId;
            this.SourceSystemOriginated = this.nexusId.SourceSystemOriginated;
            this.SystemName = this.nexusId.SystemName;
            this.MappingString = this.nexusId.Identifier;
        }

        public bool CanSave { get; private set; }

        public bool DefaultReverseInd
        {
            get
            {
                return this.defaultReverseInd;
            }

            set
            {
                this.ChangeProperty(() => this.DefaultReverseInd, ref this.defaultReverseInd, value);
            }
        }

        public bool DefaultReverseIndChanged
        {
            get
            {
                return this.nexusId.DefaultReverseInd == null
                           ? false
                           : this.defaultReverseInd != this.nexusId.DefaultReverseInd;
            }
        }

        public string ETag { get; set; }

        public DateTime EndDate
        {
            get
            {
                return this.endDate;
            }

            set
            {
                this.ChangeProperty(() => this.EndDate, ref this.endDate, value);
            }
        }

        public bool Identifier
        {
            get
            {
                return this.identifier;
            }

            set
            {
                this.ChangeProperty(() => this.Identifier, ref this.identifier, value);
            }
        }

        public bool IsClonedCopy { get; set; }

        public bool IsMdmId
        {
            get
            {
                return this.isMdmId;
            }

            set
            {
                this.ChangeProperty(() => this.IsMdmId, ref this.isMdmId, value);
            }
        }

        public int? MappingId
        {
            get
            {
                return this.mappingId;
            }

            set
            {
                this.ChangeProperty(() => this.MappingId, ref this.mappingId, value);
            }
        }

        public string MappingString
        {
            get
            {
                return this.mappingString;
            }

            set
            {
                // the following checks removed as it became necessary to enter en-dash
                // value = value.Replace('\x00A0', '\x0020');    //no-break space to space
                // value = value.Replace('\x2013', '\x002D');    //en-dash to hyphen
                this.ChangeProperty(() => this.MappingString, ref this.mappingString, value);
            }
        }

        public bool MappingStringChanged
        {
            get
            {
                return this.mappingString != this.nexusId.Identifier;
            }
        }

        public bool SourceSystemOriginated
        {
            get
            {
                return this.sourceSystemOriginated;
            }

            set
            {
                this.ChangeProperty(() => this.SourceSystemOriginated, ref this.sourceSystemOriginated, value);
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this.startDate;
            }

            set
            {
                this.ChangeProperty(() => this.StartDate, ref this.startDate, value);
            }
        }

        public string SystemName
        {
            get
            {
                return this.systemName;
            }

            set
            {
                this.ChangeProperty(() => this.SystemName, ref this.systemName, value);
            }
        }

        public MdmId Model()
        {
            return new MdmId
                       {
                           DefaultReverseInd = this.DefaultReverseInd, 
                           IsMdmId = this.IsMdmId, 
                           EndDate = this.EndDate, 
                           StartDate = this.StartDate, 
                           MappingId = this.MappingId, 
                           SourceSystemOriginated = this.SourceSystemOriginated, 
                           SystemName = this.SystemName, 
                           Identifier = this.MappingString
                       };
        }

        public MdmId OriginalModel()
        {
            this.nexusId.MappingId = null;
            return this.nexusId;
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
            if (this.IsMdmId)
            {
                return false;
            }

            return
                !(this.nexusId.DefaultReverseInd == this.DefaultReverseInd && this.nexusId.EndDate == this.EndDate
                  && this.nexusId.Identifier == this.MappingString && this.nexusId.IsMdmId == this.IsMdmId
                  && this.nexusId.SourceSystemOriginated == this.SourceSystemOriginated
                  && this.nexusId.StartDate == this.StartDate && this.nexusId.SystemName == this.SystemName);
        }

        private MdmId NewMdmId()
        {
            return new MdmId
                       {
                           StartDate = DateUtility.MinDate, 
                           EndDate = DateUtility.MaxDate, 
                           DefaultReverseInd = false, 
                           SystemName = string.Empty, 
                           Identifier = string.Empty
                       };
        }
    }
}