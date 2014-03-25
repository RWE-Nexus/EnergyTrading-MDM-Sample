﻿// This code was generated by a tool: ViewModelTemplates\EntityViewModelTemplate.tt
namespace Admin.SourceSystemModule.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using Common.Events;
    using Common.Extensions;
    using Common.Framework;
    using Common.Services;

    using EnergyTrading.Mdm.Contracts;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.ViewModel;

    public class SourceSystemViewModel : NotificationObject
    {
        private readonly IEventAggregator eventAggregator;

        private readonly SourceSystem sourcesystem;

        private bool canSave;

        private DateTime end;

        private string name;

        private int? parentId;

        private string parentName;

        private DateTime start;

        public SourceSystemViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.sourcesystem = new SourceSystem
                                    {
                                        MdmSystemData =
                                            new SystemData
                                                {
                                                    StartDate = DateUtility.MinDate, 
                                                    EndDate = DateUtility.MaxDate
                                                }
                                    };

            this.Start = this.sourcesystem.MdmSystemData.StartDate.Value;

            this.End = this.sourcesystem.MdmSystemData.EndDate.Value;
        }

        public SourceSystemViewModel(EntityWithETag<SourceSystem> ewe, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.sourcesystem = ewe.Object;

            this.Id = this.sourcesystem.MdmId();
            this.ETag = ewe.ETag;

            if (this.sourcesystem.MdmSystemData != null && this.sourcesystem.MdmSystemData.StartDate != null)
            {
                this.Start = this.sourcesystem.MdmSystemData.StartDate.Value;
            }

            if (this.sourcesystem.MdmSystemData != null && this.sourcesystem.MdmSystemData.EndDate != null)
            {
                this.End = this.sourcesystem.MdmSystemData.EndDate.Value;
            }

            this.Name = this.sourcesystem.Details.Name;

            this.ParentId = this.sourcesystem.Details.Parent.MdmId();

            this.ParentName = this.sourcesystem.Details.Parent != null ? this.sourcesystem.Details.Parent.Name : null;
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

        public string ETag { get; private set; }

        public DateTime End
        {
            get
            {
                return this.end;
            }

            set
            {
                this.ChangeProperty(() => this.End, ref this.end, value);
            }
        }

        public int? Id { get; private set; }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.ChangeProperty(() => this.Name, ref this.name, value);
            }
        }

        public int? ParentId
        {
            get
            {
                return this.parentId;
            }

            set
            {
                this.ChangeProperty(() => this.ParentId, ref this.parentId, value);
            }
        }

        public string ParentName
        {
            get
            {
                return this.parentName;
            }

            set
            {
                this.parentName = value;
                this.RaisePropertyChanged(() => this.ParentName);
            }
        }

        public DateTime Start
        {
            get
            {
                return this.start;
            }

            set
            {
                this.ChangeProperty(() => this.Start, ref this.start, value);
            }
        }

        public SourceSystem Model()
        {
            return new SourceSystem
                       {
                           Details =
                               new SourceSystemDetails
                                   {
                                       Name = this.Name, 
                                       Parent =
                                           this.ParentId == null
                                               ? null
                                               : new EntityId
                                                     {
                                                         Identifier =
                                                             new MdmId
                                                                 {
                                                                     IsMdmId
                                                                         =
                                                                         true, 
                                                                     Identifier
                                                                         =
                                                                         this
                                                                         .ParentId
                                                                         .ToString
                                                                         (
                                                                             )
                                                                 }
                                                     }
                                   }, 
                           MdmSystemData = new SystemData { StartDate = this.Start, EndDate = this.End }
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
                !(this.sourcesystem.MdmSystemData.StartDate == this.Start
                  && this.sourcesystem.MdmSystemData.EndDate == this.End && this.sourcesystem.Details.Name == this.Name
                  && this.ParentHasNoChanges());
        }

        private bool ParentHasNoChanges()
        {
            int? id;
            if (this.sourcesystem.Details.Parent == null)
            {
                id = null;
            }
            else
            {
                id = this.sourcesystem.Details.Parent.Identifier.Identifier.ParseToNullableInt();
            }

            return id == this.parentId;
        }
    }
}