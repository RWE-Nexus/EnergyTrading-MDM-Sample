namespace MDM.Loader.FakeEntities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [CollectionDataContract(Namespace = "http://schemas.rwe.com/nexus", ItemName = "PortfolioHierarchyFake")]
    [XmlRoot(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class PortfolioHierarchyFakeList : List<PortfolioHierarchyFake>
    {
    }
}