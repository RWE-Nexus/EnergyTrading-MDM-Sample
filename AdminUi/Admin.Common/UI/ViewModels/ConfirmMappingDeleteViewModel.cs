namespace Common.UI.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Common.Events;
    using Common.Extensions;

    using Microsoft.Practices.Prism;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;

    public class ConfirmMappingDeleteViewModel : NotificationObject, IActiveAware
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IRegionManager regionManager;

        private bool isActive;

        private string mappingString;

        private string systemName;

        public ConfirmMappingDeleteViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
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

                    SystemName = parameters[NavigationParameters.SystemName];
                    MappingString = parameters[NavigationParameters.MappingValue];
                }
            }
        }

        public string MappingString
        {
            get
            {
                return mappingString;
            }

            set
            {
                if (value == mappingString)
                {
                    return;
                }

                mappingString = value;
                RaisePropertyChanged(() => MappingString);
            }
        }

        public string SystemName
        {
            get
            {
                return systemName;
            }

            set
            {
                if (value.Equals(systemName))
                {
                    return;
                }

                systemName = value;
                RaisePropertyChanged(() => SystemName);
            }
        }

        public void OnCancel()
        {
            this.eventAggregator.Publish(MappingDeleteConfirmedEvent.ForCancellation());
        }

        public void OnOk()
        {
            var parameters =
                (IDictionary<string, string>)this.regionManager.Regions[RegionNames.MappingUpdateRegion].Context;

            var mappingId = Convert.ToInt32(parameters[NavigationParameters.MappingId]);

            this.eventAggregator.Publish(new MappingDeleteConfirmedEvent(mappingId));
        }
    }
}