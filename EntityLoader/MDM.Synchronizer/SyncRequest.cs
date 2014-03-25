namespace MDM.Sync
{
    using OpenNexus.MDM.Contracts; using EnergyTrading.Mdm.Contracts;

    public class SyncRequest<T>
    {
        public MdmId SourceIdentifier { get; set; }

        public T Entity { get; set; }
    }
}