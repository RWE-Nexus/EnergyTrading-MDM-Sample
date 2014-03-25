using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MDM.Loader.FakeEntities
{
    [CollectionDataContract(Namespace = "http://schemas.rwe.com/nexus", ItemName = "ReferenceDataFake")]
    public class ReferenceDataFakeList : List<ReferenceDataFake>
    {
        
    }
}
