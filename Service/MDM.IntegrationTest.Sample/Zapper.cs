﻿namespace EnergyTrading.MDM.Test
{
    using System.Collections.Generic;

    using EnergyTrading.Data;

    public class Zapper : EnergyTrading.Test.Data.Zapper
    {
        public Zapper(IDao dao) : base(dao)
        {            
        }

        public static IEnumerable<string> UpdateCommands
        {
            get
            {
                return new string [] { };
            }
        }

        public static IEnumerable<string> Tables
        {
            get
            {
                return new[] 
                { 
                    // TODO_UnitTestGeneration - add new tables for entity and mappings
                    //for Agreement
                    "dbo.AgreementMappingAudit",
                    "dbo.AgreementMapping",
                    "dbo.AgreementAudit",
                    "dbo.Agreement",

                    // must be before book due to FK constraints
                    "dbo.BookDefaultMappingAudit",
                    "dbo.BookDefaultMapping",
                    "dbo.BookDefaultAudit",
                    "dbo.BookDefault",

                    "dbo.BookMappingAudit",
                    "dbo.BookMapping",
                    "dbo.BookAudit",
                    "dbo.Book",


                    "dbo.ProductScotaMappingAudit",
                    "dbo.ProductScotaMapping",
                    "dbo.ProductScotaAudit",
                    "dbo.ProductScota",

                    "dbo.BrokerCommodityMappingAudit",
                    "dbo.BrokerCommodityMapping",
                    "dbo.BrokerCommodityAudit",
                    "dbo.BrokerCommodity",

                    "dbo.BrokerRateMappingAudit",
                    "dbo.BrokerRateMapping",
                    "dbo.BrokerRateDetailsAudit",
                    "dbo.BrokerRateDetails",
                    "dbo.BrokerRateAudit",
                    "dbo.BrokerRate",
                    
                    "dbo.PartyAccountabilityMapping",
                    "dbo.PartyAccountability",

                    "dbo.PartyCommodityMappingAudit",
                    "dbo.PartyCommodityMapping",
                    "dbo.PartyCommodityAudit",
                    "dbo.PartyCommodity",

                    "dbo.PartyOverrideMappingAudit",
                    "dbo.PartyOverrideMapping",
                    "dbo.PartyOverrideAudit",
                    "dbo.PartyOverride",

                    "dbo.InstrumentTypeOverrideMappingAudit",
                    "dbo.InstrumentTypeOverrideMapping",
                    "dbo.InstrumentTypeOverrideAudit",
                    "dbo.InstrumentTypeOverride",

                    "dbo.CommodityInstrumentTypeMappingAudit",
                    "dbo.CommodityInstrumentTypeMapping",
                    "dbo.CommodityInstrumentTypeAudit",
                    "dbo.CommodityInstrumentType",

                    "dbo.CommodityFeeTypeMappingAudit",
                    "dbo.CommodityFeeTypeAudit",
                    "dbo.CommodityFeeTypeMapping",
                    "dbo.CommodityFeeType",

                    "dbo.PortfolioHierarchyMappingAudit",
                    "dbo.PortfolioHierarchyMapping",
                    "dbo.PortfolioHierarchyAudit",
                    "dbo.PortfolioHierarchy",

                    "dbo.PortfolioMappingAudit",
                    "dbo.PortfolioMapping",
                    "dbo.PortfolioAudit",
                    "dbo.Portfolio",

                    "dbo.HierarchyMappingAudit",
                    "dbo.HierarchyMapping",
                    "dbo.HierarchyAudit",
                    "dbo.Hierarchy",

                    //Curve
                    "dbo.ProductCurveAudit",
                    "dbo.ProductCurveMapping",
                    "dbo.ProductCurve",

                    "dbo.CurveAudit",
                    "dbo.CurveMapping",
                    "dbo.Curve",

                    // Tenor
                    "dbo.TenorMappingAudit",
                    "dbo.TenorMapping",
                    "dbo.TenorAudit",
                    "dbo.Tenor",
                    "dbo.ProductTenorTypeMappingAudit",
                    "dbo.ProductTenorTypeMapping",
                    "dbo.ProductTenorTypeAudit",
                    "dbo.ProductTenorType",
                    "dbo.TenorTypeMappingAudit",
                    "dbo.TenorTypeMapping",
                    "dbo.TenorTypeAudit",
                    "dbo.TenorType",

                    // Product bits
                    "dbo.InstrumentTypeMappingAudit",
                    "dbo.InstrumentTypeMapping",
                    "dbo.InstrumentTypeAudit",
                    "dbo.InstrumentType",
                    "dbo.FeeTypeMappingAudit",
                    "dbo.FeeTypeAudit",
                    "dbo.FeeTypeMapping",
                    "dbo.FeeType",
                    "dbo.ProductTypeInstanceMappingAudit",
                    "dbo.ProductTypeInstanceMapping",
                    "dbo.ProductTypeInstanceAudit",
                    "dbo.ProductTypeInstance",
                    "dbo.ProductTypeMappingAudit",
                    "dbo.ProductTypeMapping",
                    "dbo.ProductTypeAudit",
                    "dbo.ProductType",
                    "dbo.ProductMappingAudit",
                    "dbo.ProductMapping",
                    "dbo.ProductAudit",
                    "dbo.Product",
                    "dbo.MarketMappingAudit",
                    "dbo.MarketMapping",
                    "dbo.MarketAudit",
                    "dbo.Market",

                    // Shape
                    "dbo.ShapeDayMappingAudit",
                    "dbo.ShapeDayMapping",
                    "dbo.ShapeDayAudit",
                    "dbo.ShapeDay",
                    "dbo.ShapeElementMappingAudit",
                    "dbo.ShapeElementMapping",
                    "dbo.ShapeElementAudit",
                    "dbo.ShapeElement",
                    "dbo.ShapeMappingAudit",
                    "dbo.ShapeMapping",
                    "dbo.ShapeAudit",
                    "dbo.Shape",

                    // Person
                    "dbo.PersonRoleAudit",
                    "dbo.PersonRole",
                    "dbo.PersonMappingAudit",
                    "dbo.PersonMapping",
                    "dbo.PersonDetailsAudit",
                    "dbo.PersonDetails",

                    // Shipper Code
                    "dbo.ShipperCodeMappingAudit",
                    "dbo.ShipperCodeMapping",
                    "dbo.ShipperCodeAudit",
                    "dbo.ShipperCode",

                    // Party                  
                    "dbo.PartyRoleAccountabilityMapping",
                    "dbo.PartyRoleAccountability",
                    "dbo.Person",
                    "dbo.PartyRoleMappingAudit",
                    "dbo.PartyRoleMapping",
                    "dbo.PartyRoleDetailsAudit",
                    "dbo.PartyRoleDetails",
                    "dbo.PartyRoleAudit",
                    "dbo.PartyRole",
                    "dbo.PartyMappingAudit",
                    "dbo.PartyMapping",
                    "dbo.PartyDetailsAudit",
                    "dbo.PartyDetails",
                    "dbo.Party",

                    // Location bits                    
                    "dbo.LocationRoleMappingAudit",
                    "dbo.LocationRoleMapping",
                    "dbo.LocationRoleAudit",
                    "dbo.LocationRole",
                    "dbo.LocationRoleTypeAudit",
                    "dbo.LocationRoleType",
                    "dbo.LocationMappingAudit",
                    "dbo.LocationMapping",
                    "dbo.LocationAudit",
                    "dbo.Location",

                    // Simple bits
                    "dbo.CommodityMappingAudit",
                    "dbo.CommodityMapping",
                    "dbo.CommodityAudit",
                    "dbo.Commodity",
                    "dbo.CalendarMappingAudit",
                    "dbo.CalendarMapping",
                    "dbo.CalendarDayAudit",
                    "dbo.CalendarDay",
                    "dbo.CalendarAudit",
                    "dbo.Calendar",

                    "dbo.UnitMappingAudit",
                    "dbo.UnitMapping",
                    "dbo.UnitAudit",
                    "dbo.Unit",

                    "dbo.DimensionMappingAudit",
                    "dbo.DimensionMapping",
                    "dbo.DimensionAudit",
                    "dbo.Dimension",

                    "dbo.VesselMappingAudit",
                    "dbo.VesselMapping",
                    "dbo.VesselAudit",
                    "dbo.Vessel",

                    "dbo.SourceSystemMappingAudit",
                    "dbo.SourceSystemMapping",
                    "dbo.SourceSystemAudit",
                    "dbo.SourceSystem",
                    "dbo.ReferenceData",
                };
            }
        }

        public void Zap()
        {
            this.Zap(UpdateCommands, Tables);
        }
    }
}