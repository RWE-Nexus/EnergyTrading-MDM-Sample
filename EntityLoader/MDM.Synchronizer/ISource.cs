namespace MDM.Sync
{
    using System.Collections.Generic;

    public interface ISource<T>
    {
        IEnumerable<SyncRequest<T>> Requests();
    }
}