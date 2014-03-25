﻿// This code was generated by a tool: ViewModelTemplates\EntityViewModelTemplate.tt
namespace Admin.BrokerModule.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using Common.Events;
    using Common.Extensions;
    using Common.Framework;
    using Common.Services;

    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Sample;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.ViewModel;

    public class BrokerViewModel : NotificationObject
    {
        private readonly Broker broker;

        private readonly IEventAggregator eventAggregator;

        private bool canSave;

        private DateTime end;

        private string fax;

        private string name;

        private int? partyId;

        private string partyName;

        private string phone;

        private DateTime start;

        public BrokerViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.broker = new Broker
                              {
                                  MdmSystemData =
                                      new SystemData
                                          {
                                              StartDate = DateUtility.MinDate, 
                                              EndDate = DateUtility.MaxDate
                                          }
                              };

            this.Start = this.broker.MdmSystemData.StartDate.Value;

            this.End = this.broker.MdmSystemData.EndDate.Value;
        }

        public BrokerViewModel(EntityWithETag<Broker> ewe, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.broker = ewe.Object;

            this.Id = this.broker.MdmId();
            this.ETag = ewe.ETag;

            if (this.broker.MdmSystemData != null && this.broker.MdmSystemData.StartDate != null)
            {
                this.Start = this.broker.MdmSystemData.StartDate.Value;
            }

            if (this.broker.MdmSystemData != null && this.broker.MdmSystemData.EndDate != null)
            {
                this.End = this.broker.MdmSystemData.EndDate.Value;
            }

            this.Name = this.broker.Details.Name;

            this.Fax = this.broker.Details.Fax;

            this.Phone = this.broker.Details.Phone;

            this.PartyId = this.broker.Party.MdmId();

            this.PartyName = this.broker.Party != null ? this.broker.Party.Name : null;
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

        public string Fax
        {
            get
            {
                return this.fax;
            }

            set
            {
                this.ChangeProperty(() => this.Fax, ref this.fax, value);
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

        public int? PartyId
        {
            get
            {
                return this.partyId;
            }

            set
            {
                this.ChangeProperty(() => this.PartyId, ref this.partyId, value);
            }
        }

        public string PartyName
        {
            get
            {
                return this.partyName;
            }

            set
            {
                this.partyName = value;
                this.RaisePropertyChanged(() => this.PartyName);
            }
        }

        public string Phone
        {
            get
            {
                return this.phone;
            }

            set
            {
                this.ChangeProperty(() => this.Phone, ref this.phone, value);
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

        public Broker Model()
        {
            return new Broker
                       {
                           Details = new BrokerDetails { Name = this.Name, Fax = this.Fax, Phone = this.Phone }, 
                           MdmSystemData = new SystemData { StartDate = this.Start, EndDate = this.End }, 
                           Party =
                               this.PartyId == null
                                   ? null
                                   : new EntityId
                                         {
                                             Identifier =
                                                 new MdmId
                                                     {
                                                         IsMdmId = true, 
                                                         Identifier = this.PartyId.ToString()
                                                     }
                                         }
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
                !(this.broker.MdmSystemData.StartDate == this.Start && this.broker.MdmSystemData.EndDate == this.End
                  && this.broker.Details.Name == this.Name && this.broker.Details.Fax == this.Fax
                  && this.broker.Details.Phone == this.Phone && this.PartyHasNoChanges());
        }

        private bool PartyHasNoChanges()
        {
            int? id;
            if (this.broker.Party == null)
            {
                id = null;
            }
            else
            {
                id = this.broker.Party.Identifier.Identifier.ParseToNullableInt();
            }

            return id == this.partyId;
        }
    }
}