namespace Admin.UnitTest.Framework
{
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public abstract class TestBase<T>
    {
        private IUnityContainer container;

        protected T Sut;

       [TestInitialize] 
       public void Setup()
       {
           this.container = new UnityContainer().AddNewExtension<AutoMockingContainerExtension>();
           this.Establish_context();
           this.Because_of();
       }

        protected virtual void Because_of()
        {
        }

        protected virtual void Establish_context()
        {
            this.Sut = this.container.Resolve<T>();
        }

        public void AddConcrete<TInterface, TInstance>(TInstance concrete) where TInstance : TInterface
        {
            this.container.RegisterInstance<TInterface>(concrete);
        }

        public TInterface Concrete<TInterface>()
        {
            return this.container.Resolve<TInterface>();
        }

        public Mock<TInterface> RegisterMock<TInterface>() where TInterface : class
        {
            return this.container.RegisterMock<TInterface>();
        }

        public Mock<TInterface> Mock<TInterface>() where TInterface : class
        {
            return this.container.ConfigureMockFor<TInterface>();
        }
    }
}