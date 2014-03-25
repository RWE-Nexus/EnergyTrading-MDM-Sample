namespace MDM.Loader.FakeEntities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [CollectionDataContract(Namespace = "http://schemas.rwe.com/nexus", ItemName = "ReferenceDataFake")]
    public class ReferenceDataFakeList : List<ReferenceDataFake>
    {
    }
}