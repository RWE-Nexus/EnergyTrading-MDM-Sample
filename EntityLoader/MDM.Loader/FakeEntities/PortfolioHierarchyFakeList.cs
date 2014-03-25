using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace MDM.Loader.FakeEntities
{
    [CollectionDataContract(Namespace = "http://schemas.rwe.com/nexus", ItemName = "PortfolioHierarchyFake")]
    [XmlRoot(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class PortfolioHierarchyFakeList : List<global::MDM.Loader.FakeEntities.PortfolioHierarchyFake>
    {
        
    }
}
