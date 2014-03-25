namespace Common.Events
{
    public class MappingDeleteConfirmedEvent
    {
        public static MappingDeleteConfirmedEvent ForCancellation()
        {
            return new MappingDeleteConfirmedEvent(0)
            {
                Cancelled = true
            };
        }

        public MappingDeleteConfirmedEvent(int mappingId)
        {
            this.MappingId = mappingId;
        }

        public int MappingId { get; private set; }

        public bool Cancelled { get; private set; }
    }
}