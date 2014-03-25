﻿using EnergyTrading.Logging;

namespace EnergyTrading.MDM.Test.Web
{
    using EnergyTrading.MDM.ServiceHost.Wcf.Sample;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using NUnit.Framework;

    [TestFixture]
    public class ContractRequestFixture<TContract, TEntity>
        where TContract : class
    {
        protected UnityContainer Container { get; set; }

        [TestFixtureSetUp]
        public void Setup()
        {
            Container = new UnityContainer();

            // Self-register and set up service location 
            Container.RegisterInstance<IUnityContainer>(Container);
            var locator = new UnityServiceLocator(Container);
            Global.ServiceLocator = locator;
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
