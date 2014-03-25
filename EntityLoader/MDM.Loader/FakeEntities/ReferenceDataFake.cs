using System.Runtime.Serialization;

namespace MDM.Loader.FakeEntities
{
    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    public class ReferenceDataFake
    {
        [DataMember(Order = 1)]
        public string Key { get; set; }

        [DataMember(Order = 2)]
        public string Value { get; set; }
    }
}