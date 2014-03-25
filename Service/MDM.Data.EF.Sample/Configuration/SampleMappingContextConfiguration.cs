namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using EnergyTrading.Configuration;
    using EnergyTrading.Container.Unity;
    using EnergyTrading.Data.EntityFramework;

    using Microsoft.Practices.Unity;

    public class SampleMappingContextConfiguration : IGlobalConfigurationTask
    {
        private readonly IUnityContainer container;

        public SampleMappingContextConfiguration(IUnityContainer container)
        {
            this.container = container;
        }

        public IList<Type> DependsOn
        {
            get
            {
                return new List<Type> { };
            }
        }

        public void Configure()
        {
            // Stops EF from trying to modify the schema
            Database.SetInitializer(new NullDatabaseInitializer<SampleMappingContext>());

            this.container.RegisterInstance<Func<DbContext>>(() => new SampleMappingContext());
            this.container.RegisterType<IDbContextProvider, DbContextProvider>(CallContextLifetimeFactory.Manager());
        }
    }
}