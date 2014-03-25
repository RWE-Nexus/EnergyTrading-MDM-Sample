namespace EnergyTrading.MDM.Contracts.Sample
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    using EnergyTrading.Mdm.Contracts;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class LocationDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Type { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public EntityId Parent { get; set; }
    }
}