namespace MDM.Loader.AdcSync
{
    using System.Linq;

    public class ADCBrokerRateService
    {
        private BrokerRatesDataContext dbContext;

        public ADCBrokerRateService()
        {
            this.dbContext = new BrokerRatesDataContext();
        }

        public IQueryable FetchBrokerRates()
        {
            var brokerRates = from bd in this.dbContext.Broker_Desks
                              join broker in this.dbContext.BrokerMapValues on bd.BrokerID equals broker.ID
                              join desk in this.dbContext.lkupDesks on bd.DeskID equals desk.DeskID
                              join commodity in this.dbContext.lkupCommodities on bd.CommodityID equals commodity.CommodityID
                              join location in this.dbContext.LocationMapValues on bd.LocationMapValuesID equals location.ID
                                  into result1 /* left outer join bcoz optional */
                              from r1 in result1.DefaultIfEmpty()
                              join initAgg in this.dbContext.lkupInitAggs on bd.InitAggID equals initAgg.InitAggID
                                  into result2 /* left outer join bcoz optional */
                              from r2 in result2.DefaultIfEmpty()
                              join tradeType in this.dbContext.lkupTradeTypes on bd.TradeTypeID equals tradeType.TradeTypeID
                                  into result3 /* left outer join bcoz optional */
                              from r3 in result3.DefaultIfEmpty()
                              join sourceSystem in this.dbContext.lkupExtSources on broker.ExtSourceID equals sourceSystem.ExtSourceID
                                  into result4 /* left outer join bcoz optional */
                              from r4 in result4.DefaultIfEmpty()
                              select
                                  new
                                      {
                                          bd.BrokerDeskID,
                                          bd.DefBrokerCommission,
                                          broker.BrokerName,
                                          r4.ExtSourceName,
                                          desk.DeskName,
                                          r1.LocationName,
                                          r2.InitAggName,
                                          r3.TradeTypeCode,
                                          r3.TradeTypeDesc
                                      };

            var ssdfa = brokerRates.ToList();
            return brokerRates;
        }
    }
}