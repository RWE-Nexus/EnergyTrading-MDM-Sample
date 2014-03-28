namespace MDM.Loader.NexusClient
{
    using System.Collections.Generic;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Mdm.Contracts;

    public interface IMdmClient
    {
        WebResponse<TContract> Create<TContract>(TContract contract) where TContract : IMdmEntity;

        WebResponse<MdmId> CreateMapping<TContract>(
            int id, 
            string systemName, 
            string mappingString, 
            bool defaultSystemMapping = false) where TContract : IMdmEntity;

        WebResponse<TContract> DeleteMapping<TContract>(int id, int mappingId) where TContract : IMdmEntity;

        WebResponse<TContract> Get<TContract>(int id) where TContract : IMdmEntity;

        WebResponse<TContract> Get<TContract>(MdmId identifier) where TContract : IMdmEntity;

        WebResponse<MdmId> GetMapping<TContract>(int id, string systemName, string mappingString)
            where TContract : IMdmEntity;

        WebResponse<IList<TContract>> Search<TContract>(Search search) where TContract : IMdmEntity;

        WebResponse<TContract> Update<TContract>(int id, TContract entity, string etag) where TContract : IMdmEntity;
    }
}