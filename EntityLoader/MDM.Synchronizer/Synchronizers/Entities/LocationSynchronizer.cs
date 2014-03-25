﻿namespace MDM.Sync.Synchronizers.Entities
{
    using EnergyTrading.Mdm.Client.Services;
    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;

    public class LocationSynchronizer : MdmEntitySynchronizer<Location>
    {
        public LocationSynchronizer(IMdmService mdmService) : base(mdmService)
        {
        }
    }
}