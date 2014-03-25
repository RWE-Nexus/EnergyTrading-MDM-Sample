namespace MDM.Sync.Synchronizers.Entities
{
    using System.Linq;

    using EnergyTrading.Mdm.Client.Services;
    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;

    /// <summary>
    /// Synchronizer for an MDM entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MdmEntitySynchronizer<T> : EntitySynchronizer<T>
        where T : class, IMdmEntity
    {
        private readonly IMdmService mdmService;

        public MdmEntitySynchronizer(IMdmService mdmService)
        {
            this.mdmService = mdmService;
        } 

        protected override void Create(SyncRequest<T> request)
        {            
        }

        protected override void Update(SyncRequest<T> request, T entity)
        {            
        }

        protected override T Find(SyncRequest<T> request)
        {
            return MdmFind<T>(request.SourceIdentifier) 
                ?? this.FindById(request.Entity) 
                ?? this.FindByDetails(request.Entity);
        }

        protected virtual T FindByDetails(T entity)
        {
            return null;
        }

        protected T FindById(T entity)
        {
            return entity.Identifiers
                   .Select(this.MdmFind<T>)
                   .FirstOrDefault(candidate => candidate != null);
        }

        protected TEntity MdmFind<TEntity>(MdmId identifier)
            where TEntity : IMdmEntity
        {
            return mdmService.Get<TEntity>(identifier).Message;
        }
    }
}