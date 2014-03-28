namespace MDM.Client.Sample.IntegrationTests
{
    using EnergyTrading.Mdm.Client.Services;

    using Microsoft.Practices.Unity;

    public abstract class MdmServiceIntegrationTestBase : IntegrationTestBase
    {
        protected IMdmService MdmService { get; set; }

        protected override void OnSetup()
        {
            base.OnSetup();

            this.MdmService = this.Container.Resolve<IMdmService>();
        }
    }
}