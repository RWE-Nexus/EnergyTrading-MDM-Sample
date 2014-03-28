namespace MDM.Loader.Tests
{
    using System;
    using System.IO;

    using MDM.Sync.Loaders;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Logging;

    using SharpTestsEx;

    [TestClass]
    public class MDMLoaderFactoryBehaviour
    {
        private TestLogger logger = new TestLogger();

        [TestInitialize]
        public void Setup()
        {
            LoggerFactory.SetProvider(() => new SimpleLoggerFactory(logger));
            LoggerFactory.Initialize();
        }

        [TestMethod]
        public void ShouldRaiseErrorWhenEntityNameIsNullOrEmpty()
        {
            // Given
            ICreateMDMLoader loaderFactory = new MDMLoaderFactory();

            // When
            var loader = loaderFactory.Create(string.Empty, @"<abc></abc>", false);
            
            // Then
            loader.Should().Be.Null();
            this.logger.Error.Should().Contain("Entity name is either null or empty string.");
        }

        [TestMethod]
        public void ShouldRaiseErrorWhenEntitiesXMlIsNullOrEmpty()
        {
            // Given
            ICreateMDMLoader loaderFactory = new MDMLoaderFactory();

            // When
            var loader = loaderFactory.Create("abc", string.Empty, false);

            // Then
            loader.Should().Be.Null();
            this.logger.Error.Should().Contain("Invalid entities xml");
        }

        [TestMethod]
        public void ShouldReturnValidLoaderBasedOnTypeOfEntity()
        {
            // Given
            ICreateMDMLoader loaderFactory = new MDMLoaderFactory();

            // When
            var loader = loaderFactory.Create("Location", this.GetLocationsXmlFile(), false);

            // Then
            loader.Should().Not.Be.Null();
            loader.GetType().Should().Be(typeof(LocationLoader));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ShouldThrowNotImplementedExceptionForUnknownEntityType()
        {
            // Given
            ICreateMDMLoader loaderFactory = new MDMLoaderFactory();

            // When
            var loader = loaderFactory.Create("LocationBlah", this.GetLocationsXmlFile(), false);

            // Then
            
        }

        private string GetLocationsXmlFile()
        {
            string xml =
                @"<LocationList xmlns='http://schemas.rwe.com/nexus'>
                            <Location>
                                <Identifiers>
                                    <ReferenceID>
                                        <SystemName>Spreadsheet</SystemName>
                                        <OriginatingSystemIND>true</OriginatingSystemIND>
                                        <Identifier>1</Identifier>
                                    </ReferenceID>
                                </Identifiers>
                                <Details>
                                    <Name>Sveeden</Name>
                                </Details>
                            <Nexus>
                                <StartDate>1753-01-01T00:00:00</StartDate>
                                <EndDate>9999-12-31T00:00:00</EndDate>
                            </Nexus>
                            </Location>
                    </LocationList>";

            var filePath = Path.ChangeExtension(Path.GetTempFileName(), "xml");
            Console.WriteLine("temp file path:{0}", filePath);

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(xml);
            }

            return filePath;
        }
    }
}
