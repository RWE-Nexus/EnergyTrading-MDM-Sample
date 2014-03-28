namespace MDM.Loader.Configuration
{
    using System.Linq;

    using EnergyTrading.Configuration;

    using Microsoft.Practices.Unity;

    public static class MDMLoaderConfiguration
    {
        public static UnityContainer Configure()
        {
            // Self-register and set up service location 
            var container = ContainerConfiguration.Create();

            // Register all IGlobalConfigurationTasks from this and any other appropriate assemblies
            // NB This needs fixing so we don't have to name them - needs specific IDependencyResolver
            // see http://www.chrisvandesteeg.nl/2009/04/16/making-unity-work-more-like-the-others/
            container.RegisterType<IGlobalConfigurationTask, ServiceConfiguration>("c");
            container.RegisterType<IGlobalConfigurationTask, MdmServiceConfiguration>("d");

            // Now get them all, and initialize them, bootstrapper takes care of ordering
            var globalTasks = container.ResolveAll<IGlobalConfigurationTask>();
            var tasks = globalTasks.Select(task => task as IConfigurationTask).ToList();

            ConfigurationBootStrapper.Initialize(tasks);

            return container;
        }
    }
}