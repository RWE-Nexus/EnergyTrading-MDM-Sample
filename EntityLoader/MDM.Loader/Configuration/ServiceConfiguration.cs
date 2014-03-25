namespace MDM.Loader.Configuration
{
    using System;
    using System.Collections.Generic;

    using MDM.Loader.NexusClient;

    using Microsoft.Practices.Unity;

    using EnergyTrading.Configuration;

    public class ServiceConfiguration : IGlobalConfigurationTask
    {
        private readonly IUnityContainer container;

        public ServiceConfiguration(IUnityContainer container)
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
            // Logging
            //this.container.RegisterType<ILogger, DebugLogger>(
            //    new ContainerControlledLifetimeManager(),
            //    new InjectionConstructor(new ResolvedParameter<ILogger>("mdmLoaderLogger")));

            // Loader MDM client
            this.container.RegisterType<IMdmClient, MdmClient>();

            this.container.RegisterType<ICreateMDMLoader, MDMLoaderFactory>();
            this.container.RegisterType<IMDMDataLoaderService, MDMDataLoaderService>();
        }
    }
}
