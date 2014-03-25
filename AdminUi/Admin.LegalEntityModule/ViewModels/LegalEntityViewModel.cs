﻿// This code was generated by a tool: ViewModelTemplates\EntityViewModelTemplate.tt
namespace Admin.LegalEntityModule.ViewModels
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

    public class LegalEntityViewModel : NotificationObject
    {
        private readonly IEventAggregator eventAggregator;

        private readonly LegalEntity legalentity;

        private string address;

        private bool canSave;

        private string countryofincorporation;

        private string customerAddress;

        private string email;

        private DateTime end;

        private string fax;

        private string invoiceSetup;

        private string name;

        private int? partyId;

        private string partyName;

        private string partystatus;

        private string phone;

        private string registeredname;

        private string registrationnumber;

        private DateTime start;

        private string vendorAddress;

        private string website;

        public LegalEntityViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.legalentity = new LegalEntity
                                   {
                                       MdmSystemData =
                                           new SystemData
                                               {
                                                   StartDate = DateUtility.MinDate, 
                                                   EndDate = DateUtility.MaxDate
                                               }
                                   };

            this.Start = this.legalentity.MdmSystemData.StartDate.Value;

            this.End = this.legalentity.MdmSystemData.EndDate.Value;
        }

        public LegalEntityViewModel(EntityWithETag<LegalEntity> ewe, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.legalentity = ewe.Object;

            this.Id = this.legalentity.MdmId();
            this.ETag = ewe.ETag;

            if (this.legalentity.MdmSystemData != null && this.legalentity.MdmSystemData.StartDate != null)
            {
                this.Start = this.legalentity.MdmSystemData.StartDate.Value;
            }

            if (this.legalentity.MdmSystemData != null && this.legalentity.MdmSystemData.EndDate != null)
            {
                this.End = this.legalentity.MdmSystemData.EndDate.Value;
            }

            this.Name = this.legalentity.Details.Name;

            this.RegisteredName = this.legalentity.Details.RegisteredName;

            this.RegistrationNumber = this.legalentity.Details.RegistrationNumber;

            this.Address = this.legalentity.Details.Address;

            this.Website = this.legalentity.Details.Website;

            this.Email = this.legalentity.Details.Email;

            this.Fax = this.legalentity.Details.Fax;

            this.Phone = this.legalentity.Details.Phone;

            this.CountryOfIncorporation = this.legalentity.Details.CountryOfIncorporation;

            this.PartyStatus = this.legalentity.Details.PartyStatus;

            this.PartyId = this.legalentity.Party.MdmId();

            this.PartyName = this.legalentity.Party != null ? this.legalentity.Party.Name : null;

            this.InvoiceSetup = this.legalentity.Details.InvoiceSetup;

            this.CustomerAddress = this.legalentity.Details.CustomerAddress;

            this.VendorAddress = this.legalentity.Details.VendorAddress;
        }

        public string Address
        {
            get
            {
                return this.address;
            }

            set
            {
                this.ChangeProperty(() => this.Address, ref this.address, value);
            }
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

        public string CountryOfIncorporation
        {
            get
            {
                return this.countryofincorporation;
            }

            set
            {
                this.ChangeProperty(() => this.CountryOfIncorporation, ref this.countryofincorporation, value);
            }
        }

        public string CustomerAddress
        {
            get
            {
                return this.customerAddress;
            }

            set
            {
                this.ChangeProperty(() => this.CustomerAddress, ref this.customerAddress, value);
            }
        }

        public string ETag { get; private set; }

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                this.ChangeProperty(() => this.Email, ref this.email, value);
            }
        }

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

        public string InvoiceSetup
        {
            get
            {
                return this.invoiceSetup;
            }

            set
            {
                this.ChangeProperty(() => this.InvoiceSetup, ref this.invoiceSetup, value);
            }
        }

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

        public string PartyStatus
        {
            get
            {
                return this.partystatus;
            }

            set
            {
                this.ChangeProperty(() => this.PartyStatus, ref this.partystatus, value);
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

        public string RegisteredName
        {
            get
            {
                return this.registeredname;
            }

            set
            {
                this.ChangeProperty(() => this.RegisteredName, ref this.registeredname, value);
            }
        }

        public string RegistrationNumber
        {
            get
            {
                return this.registrationnumber;
            }

            set
            {
                this.ChangeProperty(() => this.RegistrationNumber, ref this.registrationnumber, value);
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

        public string VendorAddress
        {
            get
            {
                return this.vendorAddress;
            }

            set
            {
                this.ChangeProperty(() => this.VendorAddress, ref this.vendorAddress, value);
            }
        }

        public string Website
        {
            get
            {
                return this.website;
            }

            set
            {
                this.ChangeProperty(() => this.Website, ref this.website, value);
            }
        }

        public LegalEntity Model()
        {
            return new LegalEntity
                       {
                           Details =
                               new LegalEntityDetails
                                   {
                                       Name = this.Name, 
                                       RegisteredName = this.RegisteredName, 
                                       RegistrationNumber = this.RegistrationNumber, 
                                       Address = this.Address, 
                                       Website = this.Website, 
                                       Email = this.Email, 
                                       Fax = this.Fax, 
                                       Phone = this.Phone, 
                                       CountryOfIncorporation = this.CountryOfIncorporation, 
                                       PartyStatus = this.PartyStatus, 
                                       InvoiceSetup = this.InvoiceSetup, 
                                       CustomerAddress = this.CustomerAddress, 
                                       VendorAddress = this.VendorAddress
                                   }, 
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
                !(this.legalentity.MdmSystemData.StartDate == this.Start
                  && this.legalentity.MdmSystemData.EndDate == this.End && this.legalentity.Details.Name == this.Name
                  && this.legalentity.Details.RegisteredName == this.RegisteredName
                  && this.legalentity.Details.RegistrationNumber == this.RegistrationNumber
                  && this.legalentity.Details.Address == this.Address
                  && this.legalentity.Details.Website == this.Website && this.legalentity.Details.Email == this.Email
                  && this.legalentity.Details.Fax == this.Fax && this.legalentity.Details.Phone == this.Phone
                  && this.legalentity.Details.CountryOfIncorporation == this.CountryOfIncorporation
                  && this.legalentity.Details.PartyStatus == this.PartyStatus
                  && this.legalentity.Details.InvoiceSetup == this.InvoiceSetup
                  && this.legalentity.Details.CustomerAddress == this.CustomerAddress
                  && this.legalentity.Details.VendorAddress == this.VendorAddress && this.PartyHasNoChanges());
        }

        private bool PartyHasNoChanges()
        {
            int? id;
            if (this.legalentity.Party == null)
            {
                id = null;
            }
            else
            {
                id = this.legalentity.Party.Identifier.Identifier.ParseToNullableInt();
            }

            return id == this.partyId;
        }
    }
}