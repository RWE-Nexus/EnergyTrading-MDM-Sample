namespace MDM.Loader.FakeEntities
{
    using System.Collections.Generic;

    using EnergyTrading.Mdm.Contracts;

    public class ReferenceDataBuilder
    {
        public IDictionary<string, IList<ReferenceData>> Build(List<ReferenceDataFake> fakes)
        {
            var referenceDataLists = new Dictionary<string, IList<ReferenceData>>();

            foreach (var fake in fakes)
            {
                if (!referenceDataLists.ContainsKey(fake.Key))
                {
                    referenceDataLists.Add(fake.Key, new List<ReferenceData>());
                }

                referenceDataLists[fake.Key].Add(new ReferenceData { Value = fake.Value });
            }

            return referenceDataLists;
        }
    }
}