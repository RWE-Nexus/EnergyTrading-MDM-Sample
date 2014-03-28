namespace MDM.Client.Sample.IntegrationTests
{
    using EnergyTrading.Container.Unity;
    using EnergyTrading.Logging;
    using EnergyTrading.Test;

    using Mdm.Client.Sample.Registrars;

    using Microsoft.Practices.ServiceLocation;

    public abstract class IntegrationTestBase : Fixture
    {
        protected override ICheckerFactory CreateCheckerFactory()
        {
            return new CheckerFactory();
        }

        protected override void OnSetup()
        {
            var lm = new SimpleLoggerFactory(new NullLogger());
            LoggerFactory.SetProvider(() => lm);

            var locator = this.Container.Resolve<IServiceLocator>();
            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => locator);

            new NexusMdmClientRegistrar().Register(this.Container);
        }
    }
}