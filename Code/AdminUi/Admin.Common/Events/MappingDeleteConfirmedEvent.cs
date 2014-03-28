namespace Common.Events
{
    public class MappingDeleteConfirmedEvent
    {
        public MappingDeleteConfirmedEvent(int mappingId)
        {
            this.MappingId = mappingId;
        }

        public bool Cancelled { get; private set; }

        public int MappingId { get; private set; }

        public static MappingDeleteConfirmedEvent ForCancellation()
        {
            return new MappingDeleteConfirmedEvent(0) { Cancelled = true };
        }
    }
}