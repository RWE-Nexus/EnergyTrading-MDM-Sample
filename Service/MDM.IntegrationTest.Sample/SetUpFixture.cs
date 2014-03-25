namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.ServiceModel.Web;

    using NUnit.Framework;

    using BrokerService = EnergyTrading.MDM.ServiceHost.Wcf.Sample.BrokerService;
    using CounterpartyService = EnergyTrading.MDM.ServiceHost.Wcf.Sample.CounterpartyService;
    using ExchangeService = EnergyTrading.MDM.ServiceHost.Wcf.Sample.ExchangeService;
    using LegalEntityService = EnergyTrading.MDM.ServiceHost.Wcf.Sample.LegalEntityService;
    using LocationService = EnergyTrading.MDM.ServiceHost.Wcf.Sample.LocationService;
    using PartyRoleService = EnergyTrading.MDM.ServiceHost.Wcf.Sample.PartyRoleService;
    using PartyService = EnergyTrading.MDM.ServiceHost.Wcf.Sample.PartyService;
    using PersonService = EnergyTrading.MDM.ServiceHost.Wcf.Sample.PersonService;
    using ReferenceDataService = EnergyTrading.MDM.ServiceHost.Wcf.Sample.ReferenceDataService;
    using SourceSystemService = EnergyTrading.MDM.ServiceHost.Wcf.Sample.SourceSystemService;

    [SetUpFixture]
    public class SetUpFixture
    {
        public static ObjectScript Script;
        public static Dictionary<string, string> ServiceUrl;
        private static List<WebServiceHost> webServiceHosts = new List<WebServiceHost>();

        [SetUp]
        public static void CreateServiceHost()
        {
            ServiceUrl = new Dictionary<string, string>
                {
                    { "Person", "http://127.0.0.1:8000/" }, 
                    { "Party", "http://127.0.0.1:8001/" }, 
                    { "Location", "http://127.0.0.1:8003/" },
                    { "SourceSystem", "http://127.0.0.1:8013/" },
                    { "ReferenceData", "http://127.0.0.1:8014/" },
                    { "PartyRole", "http://127.0.0.1:8022/" },
                    { "Exchange", "http://127.0.0.1:8023/" },
                    { "Broker", "http://127.0.0.1:8025/" },
                    { "Counterparty", "http://127.0.0.1:8026/" },
                    { "LegalEntity", "http://127.0.0.1:8047/" },
                };

            webServiceHosts.Add(new WebServiceHost(typeof(PersonService), new Uri(ServiceUrl["Person"])));
            webServiceHosts.Add(new WebServiceHost(typeof(LocationService), new Uri(ServiceUrl["Location"])));
            webServiceHosts.Add(new WebServiceHost(typeof(PartyService), new Uri(ServiceUrl["Party"])));
            webServiceHosts.Add(new WebServiceHost(typeof(SourceSystemService), new Uri(ServiceUrl["SourceSystem"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ReferenceDataService), new Uri(ServiceUrl["ReferenceData"])));
            webServiceHosts.Add(new WebServiceHost(typeof(PartyRoleService), new Uri(ServiceUrl["PartyRole"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ExchangeService), new Uri(ServiceUrl["Exchange"])));
            webServiceHosts.Add(new WebServiceHost(typeof(BrokerService), new Uri(ServiceUrl["Broker"])));
            webServiceHosts.Add(new WebServiceHost(typeof(CounterpartyService), new Uri(ServiceUrl["Counterparty"])));
            webServiceHosts.Add(new WebServiceHost(typeof(LegalEntityService), new Uri(ServiceUrl["LegalEntity"])));

            Script = new ObjectScript();
            Script.RunScript();

            var global = new GlobalMock();
            global.Application_Start();

            foreach(var host in webServiceHosts)
            {
                IncludeExceptionDetailInFaults(host);

                host.Open();
            }
        }

        private static void IncludeExceptionDetailInFaults(ServiceHostBase host)
        {
            var behavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();

            if (behavior == null)
            {
                behavior = new ServiceDebugBehavior();
                host.Description.Behaviors.Add(behavior);
            }

            behavior.IncludeExceptionDetailInFaults = true;
        }

        [TearDown]
        public static void CloseServiceHost()
        {
            foreach(var host in webServiceHosts)
            {
                host.Close();
            }
        }
    }

    [TestFixture]
    public class MyFixture : IntegrationTestBase
    {
        [Test]
        public void ShouldZapTheDb()
        {
            // given
            
            // when

            // then
            Assert.That(true, Is.EqualTo(true));
        }
    }
}