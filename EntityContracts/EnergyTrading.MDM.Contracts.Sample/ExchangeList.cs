namespace EnergyTrading.MDM.Contracts.Sample
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [CollectionDataContract(Namespace = "http://schemas.rwe.com/nexus", ItemName = "Exchange")]
    [XmlRoot(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class ExchangeList : List<Exchange>
    {
    }
}