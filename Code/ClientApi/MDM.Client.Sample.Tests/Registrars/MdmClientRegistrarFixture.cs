namespace Mdm.Client.Sample.Tests.Registrars
{
    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.Mdm.Client.WebApi.WebApiClient;
    using EnergyTrading.Mdm.Client.WebClient;

    using global::Mdm.Client.Sample.Registrars;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class MdmClientRegistrarFixture
    {
        private IUnityContainer container;

        [SetUp]
        public void Setup()
        {
            this.container = new UnityContainer();
            this.container.RegisterInstance(typeof (IServiceLocator), new Mock<IServiceLocator>().Object);

            new NexusMdmClientRegistrar().Register(this.container);
        }

        [Test]
        public void CanResolveHttpClientFactory()
        {
            this.container.Resolve<IHttpClientFactory>();
        }

        [Test]
        public void CanResolveMessageRequester()
        {
            this.container.Resolve<IMessageRequester>();
        }

        [Test]
        public void CanResolveMdmModelEntityService()
        {
            this.container.Resolve<IMdmModelEntityService>();
        }
    }
}
