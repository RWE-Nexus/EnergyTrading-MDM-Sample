namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class SampleMappingContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<DateRange>();

            // Stop it checking the schema
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

            modelBuilder.Configurations.Add(new LocationConfiguration());
            modelBuilder.Configurations.Add(new LocationMappingConfiguration());
            modelBuilder.Configurations.Add(new PartyConfiguration());
            modelBuilder.Configurations.Add(new PartyDetailsConfiguration());
            modelBuilder.Configurations.Add(new PartyMappingConfiguration());
            modelBuilder.Configurations.Add(new PartyRoleConfiguration());
            modelBuilder.Configurations.Add(new PartyRoleDetailsConfiguration());
            modelBuilder.Configurations.Add(new BrokerDetailsConfiguration());
            modelBuilder.Configurations.Add(new ExchangeDetailsConfiguration());
            modelBuilder.Configurations.Add(new CounterpartyDetailsConfiguration());
            modelBuilder.Configurations.Add(new PartyRoleMappingConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new PersonDetailsConfiguration());
            modelBuilder.Configurations.Add(new PersonMappingConfiguration());
            modelBuilder.Configurations.Add(new SourceSystemConfiguration());
            modelBuilder.Configurations.Add(new SourceSystemMappingConfiguration());
            modelBuilder.Configurations.Add(new ReferenceDataConfiguration());
            modelBuilder.Configurations.Add(new LegalEntityDetailsConfiguration());
        }
    }
}