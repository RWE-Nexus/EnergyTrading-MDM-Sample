namespace Common.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using Common.Events;
    using Common.Extensions;

    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Mdm.Contracts;

    using Microsoft.Practices.Prism.Events;

    public class MappingService : IMappingService
    {
        private readonly IEventAggregator eventAggregator;

        private readonly string mappingEntityUri;

        private readonly string mappingUri;

        private readonly IMessageRequester requester;

        private readonly string sourceSystemUriList;

        public MappingService(string baseUri, IMessageRequester requester, IEventAggregator eventAggregator)
        {
            this.requester = requester;
            this.eventAggregator = eventAggregator;
            this.sourceSystemUriList = baseUri + "SourceSystem/List";
            this.mappingUri = baseUri + "{0}/{1}/mapping";
            this.mappingUri = baseUri + "{0}/{1}/mapping";
            this.mappingEntityUri = this.mappingUri + "/{2}";
        }

        public WebResponse<MdmId> CreateMapping(string entityName, int id, MdmId identifier)
        {
            return this.requester.Create(string.Format(this.mappingUri, entityName, id), identifier);
        }

        public WebResponse<MdmId> DeleteMapping(string entityName, int mappingId, int entityId)
        {
            return this.requester.Delete<MdmId>(string.Format(this.mappingEntityUri, entityName, entityId, mappingId));
        }

        public EntityWithETag<MdmId> GetMapping(string entityName, int entityId, int mappingId)
        {
            WebResponse<MappingResponse> response =
                this.requester.Request<MappingResponse>(
                    string.Format(this.mappingEntityUri, entityName, entityId, mappingId));

            return
                new EntityWithETag<MdmId>(
                    response.Code == HttpStatusCode.NotFound ? null : response.Message.Mappings[0], 
                    response.Tag);
        }

        public IList<string> GetSourceSystemNames()
        {
            var sourceSystems = this.requester.Request<SourceSystemList>(this.sourceSystemUriList);

            if (sourceSystems.IsValid)
            {
                return sourceSystems.Message.Select(x => x.Details.Name).ToList();
            }

            this.eventAggregator.Publish(new ErrorEvent(sourceSystems.Fault));
            return new List<string>();
        }

        public WebResponse<MdmId> UpdateMapping(
            string entityName, 
            int mappingId, 
            int entityId, 
            EntityWithETag<MdmId> mapping)
        {
            return this.requester.Update(
                string.Format(this.mappingEntityUri, entityName, entityId, mappingId), 
                mapping.ETag, 
                mapping.Object);
        }
    }
}