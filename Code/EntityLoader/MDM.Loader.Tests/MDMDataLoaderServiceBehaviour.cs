namespace MDM.Loader.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using MDM.Sync.Loaders;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Logging;

    using SharpTestsEx;

    [TestClass]
    public class MDMDataLoaderServiceBehaviour
    {
        private TestLogger logger = new TestLogger();

        [TestInitialize]
        public void Setup()
        {
            LoggerFactory.SetProvider(() => new SimpleLoggerFactory(logger));
            LoggerFactory.Initialize();
        }

        [TestMethod]
        public void ShouldRaiseErrorIfThereIsNoLoaderForTheGivenEntity()
        {
            // Given
            var mockLoaderFactory = new Mock<ICreateMDMLoader>();
            var loaderService = new MDMDataLoaderService(mockLoaderFactory.Object);

            // When
            mockLoaderFactory.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(null as Loader);
            loaderService.Load(string.Empty, string.Empty, false);

            // Then
            logger.Error.Should().Contain("Unable to create the MDM loader for the entity");
        }

        [TestMethod]
        public void ShouldStopLoadProcessAfterLoadCompleteIfRequested()
        {
            // Given
            var mockLoaderFactory = new Mock<ICreateMDMLoader>();
            var loaderService = new MDMDataLoaderService(mockLoaderFactory.Object);
            var testLoader = new TestLoader();

            // When
            mockLoaderFactory.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(testLoader);
            loaderService.Load(string.Empty, string.Empty, false, canStopLoadProcessorOnLoadComplete: true);

            // Then
            testLoader.LoadCompletedHandlers.Should().Contain("OnLoadCompleted");
        }

        [TestMethod]
        public void ShouldNotStopLoadProcessAfterLoadCompleteByDefault()
        {
            // Given
            var mockLoaderFactory = new Mock<ICreateMDMLoader>();
            var loaderService = new MDMDataLoaderService(mockLoaderFactory.Object);
            var testLoader = new TestLoader();

            // When
            mockLoaderFactory.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(testLoader);
            loaderService.Load(string.Empty, string.Empty, false);

            // Then
            testLoader.LoadCompletedHandlers.Should().Not.Contain("OnLoadCompleted");
        }
    }

    public class TestLoader : Loader
    {
        protected override void OnLoad()
        {
            // Do Nothing in test
            Thread.Sleep(500);
        }

        public List<string> LoadCompletedHandlers
        {
            get
            {
                var delegates = this.loadCompleted.GetInvocationList();
                return delegates.Select(d => d.Method.Name).ToList();
            }
        }
    }
}