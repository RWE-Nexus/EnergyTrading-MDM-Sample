namespace EnergyTrading.MDM.Test.Configuration
{
    using global::MDM.ServiceHost.Unity.Sample.Configuration;

    using Microsoft.Practices.Unity;
    using NUnit.Framework;

    using EnergyTrading.Configuration;

    [TestFixture]
    public class PersonConfigurationFixture : EntityConfigurationFixture
    {
        protected override string EntityName
        {
            get { return "person"; }
        }

        protected override IConfigurationTask CreateConfiguration(IUnityContainer container)
        {
            return new PersonConfiguration(container);
        }
    }
}