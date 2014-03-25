namespace MDM.Client.Sample.IntegrationTests
{
    using EnergyTrading.Mdm.Client.Services;

    using Microsoft.Practices.Unity;

    public abstract class MdmEntityLocatorFactoryIntegrationTestBase : IntegrationTestBase
    {
        protected IMdmEntityLocatorService MdmEntityLocatorService { get; set; }

        protected override void OnSetup()
        {
            base.OnSetup();

            this.MdmEntityLocatorService = this.Container.Resolve<IMdmEntityLocatorService>();
        }
    }
}