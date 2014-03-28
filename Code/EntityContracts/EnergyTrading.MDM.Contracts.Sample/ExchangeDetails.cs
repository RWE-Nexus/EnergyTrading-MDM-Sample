namespace EnergyTrading.MDM.Contracts.Sample
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class ExchangeDetails
    {
        [DataMember(Order = 3)]
        [XmlElement]
        public string Fax { get; set; }

        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public string Phone { get; set; }
    }
}