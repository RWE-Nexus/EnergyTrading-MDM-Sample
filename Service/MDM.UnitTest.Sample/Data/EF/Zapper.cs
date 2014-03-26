namespace EnergyTrading.MDM.Test.Data.EF
{
    using System.Collections.Generic;
    using System.Linq;

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
                return new[] 
                { 
                    "UPDATE dbo.Location SET ParentLocationId = NULL",
                    "UPDATE dbo.SourceSystem SET ParentSourceSystemId = NULL"
                };
            }
        }

        public static IEnumerable<string> Tables
        {
            get
            {
                return new[] 
                { 
                    // Person
                    "dbo.PersonRoleAudit",
                    "dbo.PersonRole",
                    "dbo.PersonMappingAudit",
                    "dbo.PersonMapping",
                    "dbo.PersonDetailsAudit",
                    "dbo.PersonDetails",
                    "dbo.PersonAudit",
                    "dbo.Person",

                    // Party                  
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
                    "dbo.LocationMappingAudit",
                    "dbo.LocationMapping",
                    "dbo.LocationAudit",
                    "dbo.Location",

                    // Core bits
                    "dbo.SourceSystemMappingAudit",
                    "dbo.SourceSystemMapping",
                    "dbo.SourceSystemAudit",
                    "dbo.SourceSystem",                    
                    "dbo.ReferenceDataAudit",
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