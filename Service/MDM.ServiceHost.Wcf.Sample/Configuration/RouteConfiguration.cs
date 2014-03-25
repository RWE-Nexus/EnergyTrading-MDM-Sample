namespace EnergyTrading.MDM.ServiceHost.Wcf.Sample.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel.Activation;
    using System.Web;
    using System.Web.Routing;

    using EnergyTrading.Configuration;

    public class RouteConfiguration : IGlobalConfigurationTask
    {
        private readonly RouteCollection routes;

        public RouteConfiguration(RouteCollection routes)
        {
            this.routes = routes;
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
            var hostFactory = new WebServiceHostFactory();

            // TODO: Review before deployment. This allows for routes to not be added if the service is self hosted.
            if (HttpContext.Current == null)
            {
                return;
            }

            this.routes.Add(new ServiceRoute("broker", hostFactory, typeof(BrokerService)));
            this.routes.Add(new ServiceRoute("counterparty", hostFactory, typeof(CounterpartyService)));
            this.routes.Add(new ServiceRoute("exchange", hostFactory, typeof(ExchangeService)));
            this.routes.Add(new ServiceRoute("legalentity", hostFactory, typeof(LegalEntityService)));
            this.routes.Add(new ServiceRoute("location", hostFactory, typeof(LocationService)));
            this.routes.Add(new ServiceRoute("party", hostFactory, typeof(PartyService)));
            this.routes.Add(new ServiceRoute("partyrole", hostFactory, typeof(PartyRoleService)));
            this.routes.Add(new ServiceRoute("person", hostFactory, typeof(PersonService)));
            this.routes.Add(new ServiceRoute("sourcesystem", hostFactory, typeof(SourceSystemService)));
            this.routes.Add(new ServiceRoute("referencedata", hostFactory, typeof(ReferenceDataService)));
        }
    }
}