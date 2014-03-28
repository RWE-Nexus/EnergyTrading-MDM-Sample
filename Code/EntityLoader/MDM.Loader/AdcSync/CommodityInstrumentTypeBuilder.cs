namespace MDM.Loader.AdcSync
{
    using System.Linq;

    using MDM.Loader.NexusClient;

    using Microsoft.Practices.ServiceLocation;

    using EnergyTrading.Logging;
    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;

    public class CommodityInstrumentTypeBuilder
    {
        private IMdmClient client;
        protected IMdmClient Client
        {
            get { return this.client ?? (this.client = ServiceLocator.Current.GetInstance<IMdmClient>()); }
        }

        public CommodityInstrumentTypeList BuildCommodityInstrumentTypes(IQueryable data)
        {
            var commodityInstrumentTypes = new CommodityInstrumentTypeList();

            var adcCommodityList = data.Cast<PartyCrossMap>()
                                    .Where(x => x.Commodity != null)
                                    .Select(x => x.Commodity)
                                    .Distinct()
                                    .ToList();

            foreach (var adcCommodity in adcCommodityList)
            {
                if (string.IsNullOrWhiteSpace(adcCommodity))
                {
                    continue;
                }

                var nexusId = new MdmId
                {
                    Identifier = adcCommodity,
                    SourceSystemOriginated = false,
                    IsMdmId = false,
                    SystemName = "ADC"
                };

                var commodity = this.GetCommodity(adcCommodity);
                var instrumentType = this.GetInstrumentType(adcCommodity);
                var instrumentDelivery = this.GetInstrumentDelivery(adcCommodity);

                if (commodity == null)
                {
                    continue;
                }

                var commodityInstrumentType = new CommodityInstrumentType
                    {
                        Identifiers = new MdmIdList { nexusId },
                        Details =
                            {
                                Commodity = commodity,
                                InstrumentType = instrumentType,
                                InstrumentDelivery = instrumentDelivery
                            }
                    };

                commodityInstrumentTypes.Add(commodityInstrumentType);
            }

            return commodityInstrumentTypes;
        }

        private string GetInstrumentDelivery(string adcCommodity)
        {
            if (string.IsNullOrWhiteSpace(adcCommodity))
            {
                return null;
            }

            return adcCommodity.ToLower().Contains("physical") ? "Physical" : null;
        }

        private EntityId GetInstrumentType(string adcCommodity)
        {
            if (string.IsNullOrWhiteSpace(adcCommodity))
            {
                return null;
            }

            var instrumentType = string.Empty;
            var partyCrossMapCommodity = adcCommodity.ToLower();

            if (partyCrossMapCommodity.Contains("swap"))
            {
                instrumentType = "Swap";
            }
            else if (partyCrossMapCommodity.Contains("future"))
            {
                instrumentType = "Future";
            }

            if (string.IsNullOrWhiteSpace(instrumentType))
            {
                return null;
            }

            var nexusId = new MdmId { Identifier = instrumentType, SystemName = "ADC", IsMdmId = false };
            return new EntityId { Identifier = nexusId, Name = instrumentType };
        }

        private EntityId GetCommodity(string adcCommodity)
        {
            var commodity = string.Empty;

            if (string.IsNullOrWhiteSpace(adcCommodity))
            {
                return null;
            }

            var partyCrossMapCommodity = adcCommodity.ToLower();

            if (partyCrossMapCommodity.Contains("fx")) commodity = "FX";
            else if (partyCrossMapCommodity.Contains("coal")) commodity = "Coal";
            else if (partyCrossMapCommodity.Contains("carbon")) commodity = "Carbon";
            else if (partyCrossMapCommodity.Contains("gas")) commodity = "Natural Gas";
            else if (partyCrossMapCommodity.Contains("oil")) commodity = "Oil";
            else if (partyCrossMapCommodity.Contains("power")) commodity = "Power";
            else if (partyCrossMapCommodity.Contains("freightdry")) commodity = "Freight (Dry)";

            if (string.IsNullOrWhiteSpace(commodity))
            {
                return null;
            }

            var commodityId = new MdmId() { Identifier = commodity, SystemName = "ADC", IsMdmId = false };
            return new EntityId() { Identifier = commodityId, Name = commodity };
        }
    }
}
