namespace MDM.Loader.FakeEntities
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    using EnergyTrading.Mdm.Contracts;

    using OpenNexus.MDM.Contracts;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class PortfolioHierarchyDetailsFake
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public Portfolio ChildPortfolio { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public EntityId Hierarchy { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public Portfolio ParentPortfolio { get; set; }
    }
}