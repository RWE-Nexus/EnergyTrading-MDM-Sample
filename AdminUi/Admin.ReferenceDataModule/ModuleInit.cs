
namespace Admin.ReferenceDataModule
{
    using Admin.ReferenceDataModule.ViewModels;
    using Admin.ReferenceDataModule.Views;
    using Common.Services;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Unity;
    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.Mdm.Client.WebClient;
    using System;

    public class ModuleInit : IModule
    {
        private readonly IUnityContainer container;
        private readonly IApplicationMenuRegistry menuRegistry;

        public ModuleInit(IUnityContainer container, IApplicationMenuRegistry menuRegistry)
        {
            this.container = container;
            this.menuRegistry = menuRegistry;
        }

        public void Initialize()
        {
            this.Register();

            this.menuRegistry.RegisterMenuItem("ReferenceData", string.Empty, typeof(ReferenceDataSearchResultsView), new Uri(ReferenceDataViewNames.ReferenceDataAddView, UriKind.Relative), "ReferenceKey", "Reference _Key");
        }

        private void Register()
        {
            this.container.RegisterType<IReferenceDataService, ReferenceDataService>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(Server.Name + "referenceData", new ResolvedParameter<IMessageRequester>()));
            this.container.RegisterType<object, ReferenceDataEditView>(ReferenceDataViewNames.ReferenceDataEditView);
            this.container.RegisterType<object, ReferenceDataAddView>(ReferenceDataViewNames.ReferenceDataAddView);
            this.container.RegisterType<object, ReferenceDataSearchResultsView>(ReferenceDataViewNames.ReferenceDataSearchResultsView);

            this.container.RegisterType<ReferenceDataAddViewModel>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                new ResolvedParameter<IEventAggregator>(),
                new ResolvedParameter<IReferenceDataService>()
                          ));

            this.container.RegisterType<ReferenceDataEditViewModel>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                new ResolvedParameter<IEventAggregator>(),
                new ResolvedParameter<IReferenceDataService>(),
                new ResolvedParameter<INavigationService>(),
                new ResolvedParameter<IMappingService>(),
                new ResolvedParameter<IApplicationCommands>()
                          ));


        }
    }
}
