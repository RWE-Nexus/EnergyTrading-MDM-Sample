namespace Shell
{
    using System;
    using System.Windows;

    using Common;
    using Common.Services;
    using Common.UI.Uris;
    using Common.UI.ViewModels;
    using Common.UI.Views;

    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.Mdm.Client.WebApi.WebApiClient;
    using EnergyTrading.Mdm.Client.WebClient;

    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.UnityExtensions;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using Shell.Services;
    using Shell.ViewModels;
    using Shell.Views;

    public class Bootstrapper : UnityBootstrapper
    {
        private ShellView view;

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.Container.RegisterType<string>(Server.Name);

            this.Container.RegisterType<ShellViewModel>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<INavigationService, NavigationService>();
            this.Container.RegisterType<IMenuService, MenuService>();
            this.Container.RegisterType<object, SearchView>(ViewNames.SearchView);
            this.Container.RegisterType<SearchViewModel>(new ContainerControlledLifetimeManager());

            this.Container.RegisterType<IApplicationMenuRegistry, MenuRegistry>();
            this.Container.RegisterType<IApplicationCommands, ApplicationCommands>();
            this.Container.RegisterType<IHttpClientFactory, HttpClientFactory>();
            this.Container.RegisterType<IMessageRequester, MessageRequester>();
            this.Container.RegisterType<INavigationService, NavigationService>();

            this.Container.RegisterType<object, MappingEditView>(ViewNames.MappingEditView);
            this.Container.RegisterType<object, MappingAddView>(ViewNames.MappingAddView);
            this.Container.RegisterType<object, MappingCloneView>(ViewNames.MappingCloneView);

            // this.Container.RegisterType<MappingUpdateView>();
            this.Container.RegisterType<MappingEditViewModel>();
            this.Container.RegisterType<MappingAddViewModel>();
            this.Container.RegisterType<MappingCloneViewModel>();

            // this.Container.RegisterType<MappingUpdateViewModel>();
            this.Container.RegisterType<IMdmService, MdmService>();
            this.Container.RegisterType<IMdmEntityServiceFactory, LocatorMdmEntityServiceFactory>();
            this.Container.RegisterInstance<IMappingService>(
                new MappingService(
                    Server.Name, 
                    this.Container.Resolve<IMessageRequester>(), 
                    this.Container.Resolve<IEventAggregator>()));
            this.ConfigureForEnterpriseLibraryLogging();

            this.SetupSelfRegistration();
            this.CreateServiceLocator();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog moduleCatalog =
                Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
                    new Uri("/Nexus.Admin;component/ModuleCatalog.xaml", UriKind.Relative));

            return moduleCatalog;
        }

        protected override DependencyObject CreateShell()
        {
            this.view = this.Container.TryResolve<ShellView>();
            this.view.Show();

            return this.view;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            (this.view.DataContext as ShellViewModel).Initialise();

            var regionManager = this.Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate(RegionNames.MainRegion, ViewNames.SearchView);
        }

        private void ConfigureForEnterpriseLibraryLogging()
        {
            this.Container.AddNewExtension<EnterpriseLibraryCoreExtension>();
        }

        private void CreateServiceLocator()
        {
            var locator = new UnityServiceLocator(this.Container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        private void SetupSelfRegistration()
        {
            this.Container.RegisterInstance(this.Container);
        }
    }
}