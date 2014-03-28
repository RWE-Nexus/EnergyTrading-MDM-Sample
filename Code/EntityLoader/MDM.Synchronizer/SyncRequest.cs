namespace MDM.Sync
{
    using EnergyTrading.Mdm.Contracts;

    public class SyncRequest<T>
    {
        public T Entity { get; set; }

        public MdmId SourceIdentifier { get; set; }
    }
}