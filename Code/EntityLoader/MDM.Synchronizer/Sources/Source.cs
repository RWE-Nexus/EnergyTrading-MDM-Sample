namespace MDM.Sync.Sources
{
    using System.Collections.Generic;

    public abstract class Source<T> : ISource<T>
    {
        public abstract IEnumerable<SyncRequest<T>> Requests();
    }
}