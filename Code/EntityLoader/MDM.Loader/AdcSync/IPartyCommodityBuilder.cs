using System.Collections.Generic;
using System.Linq;
using MDM.Sync.Synchronizers.Adc;
using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;

namespace MDM.Loader.AdcSync
{
    internal interface IPartyCommodityBuilder
    {
        PartyCommodityList BuildPartyCommodities(IQueryable data);
    }
}