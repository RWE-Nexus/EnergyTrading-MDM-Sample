namespace Common.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Mdm.Contracts;

    public interface IMappingService
    {
        WebResponse<MdmId> CreateMapping(string entityName, int id, MdmId identifier);

        WebResponse<MdmId> DeleteMapping(string entityName, int mappingId, int entityId);

        EntityWithETag<MdmId> GetMapping(string entityName, int entityId, int mappingId);

        // WebResponse<MdmId> ReplaceMapping(string entityName, int mappingId, int entityId);
        IList<string> GetSourceSystemNames();

        WebResponse<MdmId> UpdateMapping(string entityName, int mappingId, int entityId, EntityWithETag<MdmId> mapping);
    }
}