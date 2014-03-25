namespace Common.Events
{
    public class MappingClonedEvent
    {
        private readonly int entityId;
        private readonly int mappingId;

        public MappingClonedEvent(int entityId, int mappingId)
        {
            this.entityId = entityId;
            this.mappingId = mappingId;
        }

        public int EntityId
        {
            get { return entityId; }
        }

        public int MappingId
        {
            get { return mappingId; }
        }
    }
}