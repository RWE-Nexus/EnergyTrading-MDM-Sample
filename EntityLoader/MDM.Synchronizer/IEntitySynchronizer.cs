namespace MDM.Sync
{
    /// <summary>
    /// Processes a synchronization request.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntitySynchronizer<T>
    {
        /// <summary>
        /// Takes an entity and synchronizes it to MDM
        /// </summary>
        /// <param name="request"></param>
        void Synchronize(SyncRequest<T> request);
    }
}