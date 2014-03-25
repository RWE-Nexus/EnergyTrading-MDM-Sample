namespace MDM.Sync
{
    /// <summary>
    /// Pushes synchronization requests from the source to the target
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Synchronizer<T>
    {
        private readonly ISource<T> source;
        private readonly IEntitySynchronizer<T> synchronizer;

        public Synchronizer(ISource<T> source, IEntitySynchronizer<T> synchronizer)
        {
            this.source = source;
            this.synchronizer = synchronizer;
        }

        public void Sync()
        {
            foreach (var request in source.Requests())
            {
                synchronizer.Synchronize(request);
            }
        }
    }
}