namespace MDM.Loader.AdcSync
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using MDM.Loader.NexusClient;

    using Microsoft.Practices.ServiceLocation;
    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Search;

    public class PartyCommodityBuilder : IPartyCommodityBuilder
    {
        private IMdmClient client;
        private IDictionary<string, List<MdmId>> commodityInstrumentTypeLookups;
        
        protected IMdmClient Client
        {
            get
            {
                return this.client ?? (this.client = ServiceLocator.Current.GetInstance<IMdmClient>());
            }
        }

        public PartyCommodityList BuildPartyCommodities(IQueryable data)
        {
            var partyCommodities = new PartyCommodityList();
            var crossMapNumber = 0;

            foreach (var item in data)
            {
                var partyCrossMap = item as PartyCrossMap;

                Debug.Assert(partyCrossMap != null, "partyCrossMap != null");

                if (string.IsNullOrWhiteSpace(partyCrossMap.Commodity))
                {
                    continue;
                }

                partyCommodities.AddRange(PartyCommodities(partyCrossMap, crossMapNumber));

                crossMapNumber++;
            }

            return partyCommodities;
        }

        private IEnumerable<PartyCommodity> PartyCommodities(PartyCrossMap partyCrossMap, int crossMapNumber)
        {
            var partyCommodities = new List<PartyCommodity>();
            
            var commodityInstrumentTypeIds = CommodityInstrumentTypeIds(partyCrossMap).ToList();

            for (int citNumber = 0; citNumber < commodityInstrumentTypeIds.Count; citNumber++)
            {
                var partyCommodity = new PartyCommodity
                                         {
                                             Identifiers = new MdmIdList {MdmId(partyCrossMap, crossMapNumber, citNumber)}
                                         };

                partyCommodity.Details.SourceSystem = SourceSystemId(partyCrossMap);
                partyCommodity.Details.MappingValue = MappingValue(partyCrossMap);
                partyCommodity.Details.CommodityInstrumentType = commodityInstrumentTypeIds[citNumber];
                partyCommodity.Details.Party = PartyId(partyCrossMap);
                partyCommodities.Add(partyCommodity);
            }
            return partyCommodities;
        }


        private IEnumerable<EntityId> CommodityInstrumentTypeIds(PartyCrossMap partyCrossMap)
        {
            if (!CommodityInstrumentTypeLookups.ContainsKey(partyCrossMap.Commodity))
            {
                return Enumerable.Empty<EntityId>();
            }

            return CommodityInstrumentTypeLookups[partyCrossMap.Commodity].Select(x => new EntityId { Identifier = x });
        }

        private static MdmId MdmId(PartyCrossMap partyCrossMap, int partyCrossMapNumber, int citNumber)
        {
            var id = string.Concat(partyCrossMap.MapId1, "|", partyCrossMap.MapId2, "|", partyCrossMapNumber, "|", citNumber);
            return new MdmId
            {
                SystemName = "ADC",
                Identifier = id,
                SourceSystemOriginated = false,
                IsMdmId = false
            };
        }

        private static EntityId SourceSystemId(PartyCrossMap partyCrossMap)
        {
            return new EntityId
            {
                Identifier = new MdmId { Identifier = partyCrossMap.System2, SystemName = "ADC" },
                Name = partyCrossMap.System2
            };
        }

        private static string MappingValue(PartyCrossMap partyCrossMap)
        {
            return partyCrossMap.MapValue2;
        }

        private static EntityId PartyId(PartyCrossMap partyCrossMap)
        {
            // System1 will always be "ENDUR"
            return new EntityId
            {
                Identifier = new MdmId { SystemName = partyCrossMap.System1, Identifier = partyCrossMap.MapValue1 }
            };
        }

        private IDictionary<string, List<MdmId>> CommodityInstrumentTypeLookups
        {
            get
            {
                if (this.commodityInstrumentTypeLookups == null)
                {
                    this.commodityInstrumentTypeLookups = BuildCommodityInstrumentTypeLookups();
                }
                return this.commodityInstrumentTypeLookups;
            }
        }

        private IDictionary<string, List<MdmId>> BuildCommodityInstrumentTypeLookups()
        {
            var search = SearchBuilder.CreateSearch();

            var webResponse = Client.Search<CommodityInstrumentType>(search);

            if (!webResponse.IsValid)
            {
                throw new Exception(String.Format("Unable to obtain search results for CommodityInstrumentType: {0}", webResponse.Fault.Message));
            }

            var dictionary = new Dictionary<string, List<MdmId>>();
            foreach (var result in webResponse.Message)
            {
                MdmId adcCommodityId = result.Identifiers.FirstOrDefault(x => x.SystemName == "ADC");
                if (adcCommodityId != null)
                {
                    string adcCommodity = AdcCommodity(adcCommodityId.Identifier);

                    if (!dictionary.ContainsKey(adcCommodity))
                    {
                        dictionary.Add(adcCommodity, new List<MdmId>());
                    }

                    MdmId nexusId = result.Identifiers.First(x => x.IsMdmId);
                    dictionary[adcCommodity].Add(nexusId);
                }
            }

            return dictionary;
        }

        private static string AdcCommodity(string identifier)
        {
            return identifier.Contains('|') ? identifier.Split(new[] { '|' })[0] : identifier;
        }
    }
}
