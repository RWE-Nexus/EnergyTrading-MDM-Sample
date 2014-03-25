namespace MDM.Sync.Synchronizers.Entities
{
    /// <summary>
    /// Base class for synchronizing an entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntitySynchronizer<T> : IEntitySynchronizer<T>
        where T : class
    {
        /// <summary>
        /// Synchronize a single request.
        /// </summary>
        /// <param name="request"></param>
        public void Synchronize(SyncRequest<T> request)
        {
            var entity = this.Find(request);
            if (entity == null)
            {
                this.Create(request);
                return;
            }

            this.Update(request, entity);
        }

        protected abstract void Create(SyncRequest<T> request);

        protected abstract T Find(SyncRequest<T> request);

        protected abstract void Update(SyncRequest<T> request, T entity);
    }
}