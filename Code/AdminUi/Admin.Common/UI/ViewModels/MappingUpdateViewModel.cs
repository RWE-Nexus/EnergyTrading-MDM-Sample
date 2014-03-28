namespace Common.UI.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Common.Events;
    using Common.Extensions;
    using Common.Services;

    using EnergyTrading;
    using EnergyTrading.Mdm.Contracts;

    using Microsoft.Practices.Prism;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;

    public class MappingUpdateViewModel : NotificationObject, IActiveAware
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IMappingService mappingService;

        private readonly IRegionManager regionManager;

        private bool isActive;

        private string newValue;

        private MdmId nexusId;

        private DateTime startDate;

        public MappingUpdateViewModel(
            IEventAggregator eventAggregator, 
            IRegionManager regionManager, 
            IMappingService mappingService)
        {
            this.nexusId = this.NewMdmId();
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.mappingService = mappingService;
        }

        public event EventHandler IsActiveChanged = delegate { };

        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
                if (isActive)
                {
                    var parameters =
                        (IDictionary<string, string>)this.regionManager.Regions[RegionNames.MappingUpdateRegion].Context;

                    StartDate = DateTime.Today;
                    NewValue = parameters[NavigationParameters.MappingValue];
                }
            }
        }

        public bool IsDefault { get; set; }

        public bool IsSourceSystemOriginated { get; set; }

        public string NewValue
        {
            get
            {
                return newValue;
            }

            set
            {
                if (value == newValue)
                {
                    return;
                }

                newValue = value;
                RaisePropertyChanged(() => NewValue);
            }
        }

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                if (value.Equals(startDate))
                {
                    return;
                }

                startDate = value;
                RaisePropertyChanged(() => StartDate);
            }
        }

        public void OnCancel()
        {
            this.eventAggregator.Publish(MappingUpdatedEvent.ForCancellation());
        }

        public void OnOk()
        {
            var parameters =
                (IDictionary<string, string>)this.regionManager.Regions[RegionNames.MappingUpdateRegion].Context;

            var entityId = Convert.ToInt32(parameters[NavigationParameters.EntityId]);
            var mappingId = Convert.ToInt32(parameters[NavigationParameters.MappingId]);
            var entityName = Convert.ToString(parameters[NavigationParameters.EntityName]);

            this.LoadMappingFromService(entityId, mappingId, entityName);

            // this.eventAggregator.Publish(new MappingUpdatedEvent(entityId, mappingId, NewValue, StartDate));
            // On update the existing mapping details(Isdefault, SourceSystemOriginated) should be carried to New mapping
            this.eventAggregator.Publish(
                new MappingUpdatedEvent(entityId, mappingId, NewValue, StartDate, IsDefault, IsSourceSystemOriginated));
        }

        private void LoadMappingFromService(int pid, int mappingId, string entityName)
        {
            EntityWithETag<MdmId> mappingFromService = this.mappingService.GetMapping(entityName, pid, mappingId);
            this.MapExistingMappings(mappingFromService);
        }

        private void MapExistingMappings(EntityWithETag<MdmId> ewe)
        {
            this.nexusId = ewe.Object;
            this.IsDefault = this.nexusId.DefaultReverseInd.Value;
            this.IsSourceSystemOriginated = this.nexusId.SourceSystemOriginated;
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