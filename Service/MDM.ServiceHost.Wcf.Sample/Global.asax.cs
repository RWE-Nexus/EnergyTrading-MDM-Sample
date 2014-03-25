namespace EnergyTrading.MDM.ServiceHost.Wcf.Sample
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Routing;

    using EnergyTrading.Configuration;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading.MDM.ServiceHost.Unity.Configuration;
    using EnergyTrading.MDM.ServiceHost.Wcf.Sample.Configuration;

    using global::MDM.ServiceHost.Unity.Sample.Configuration;
    using global::MDM.ServiceHost.Wcf.Configuration;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using LocationConfiguration = global::MDM.ServiceHost.Unity.Sample.Configuration.LocationConfiguration;
    using PartyConfiguration = global::MDM.ServiceHost.Unity.Sample.Configuration.PartyConfiguration;
    using PartyRoleConfiguration = global::MDM.ServiceHost.Unity.Sample.Configuration.PartyRoleConfiguration;
    using PersonConfiguration = global::MDM.ServiceHost.Unity.Sample.Configuration.PersonConfiguration;
    using SourceSystemConfiguration = EnergyTrading.MDM.ServiceHost.Unity.Configuration.SourceSystemConfiguration;

    public class Global : HttpApplication
    {
        public static IServiceLocator ServiceLocator { get; set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            // Self-register and set up service location 
            var container = ContainerConfiguration.Create();

            // Auto register all the global configuration tasks
            //container.ConfigureAutoRegistration()
            //     .Include(x => x.Implements<IGlobalConfigurationTask>(),
            //              Then.Register().As<IGlobalConfigurationTask>().WithTypeName())
            //     .ApplyAutoRegistration(new[] { GetType().Assembly, typeof(BrokerCommodityConfiguration).Assembly });

            // Register all IGlobalConfigurationTasks from this and any other appropriate assemblies
            // NB This needs fixing so we don't have to name them - needs specific IDependencyResolver
            // see http://www.chrisvandesteeg.nl/2009/04/16/making-unity-work-more-like-the-others/
            container.RegisterType<IGlobalConfigurationTask, SampleMappingContextConfiguration>("a");
            container.RegisterType<IGlobalConfigurationTask, RouteConfiguration>("b");
            container.RegisterType<IGlobalConfigurationTask, ServiceConfiguration>("c");
            container.RegisterType<IGlobalConfigurationTask, SimpleMappingEngineConfiguration>("d");
            container.RegisterType<IGlobalConfigurationTask, RepositoryConfiguration>("e");
            // container.RegisterType<IGlobalConfigurationTask, ProfilingConfiguration>("e");
            container.RegisterType<IGlobalConfigurationTask, LoggerConfiguration>("f");

            // Per-entity configurations
            container.RegisterType<IGlobalConfigurationTask, BrokerConfiguration>("broker");
            container.RegisterType<IGlobalConfigurationTask, CounterpartyConfiguration>("counterparty");
            container.RegisterType<IGlobalConfigurationTask, ExchangeConfiguration>("exchange");
            container.RegisterType<IGlobalConfigurationTask, LocationConfiguration>("location");
            container.RegisterType<IGlobalConfigurationTask, PartyConfiguration>("party");
            container.RegisterType<IGlobalConfigurationTask, PartyRoleConfiguration>("partyrole");
            container.RegisterType<IGlobalConfigurationTask, PersonConfiguration>("person");
            container.RegisterType<IGlobalConfigurationTask, SourceSystemConfiguration>("sourcesystem");
            container.RegisterType<IGlobalConfigurationTask, LegalEntityConfiguration>("legalentity");

            // Some dependencies for the tasks
            container.RegisterInstance(RouteTable.Routes);

            // Now get them all, and initialize them, bootstrapper takes care of ordering
            var globalTasks = container.ResolveAll<IGlobalConfigurationTask>();
            var tasks = globalTasks.Select(task => task as IConfigurationTask).ToList();

            ConfigurationBootStrapper.Initialize(tasks);

            ServiceLocator = container.Resolve<IServiceLocator>();
        }
    }
}