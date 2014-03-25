namespace Common.Events
{
    public class MappingUpdateEvent
    {
        public MappingUpdateEvent(int entityId, int mappingId)
            : this(entityId, mappingId, "")
        {
        }

        public MappingUpdateEvent(int entityId, int mappingId, string mappingValue)
            : this(entityId, mappingId, mappingValue, "")
        {
            EntityId = entityId;
            MappingId = mappingId;
            MappingValue = mappingValue;
        }

        public MappingUpdateEvent(int entityId, int mappingId, string mappingValue, string entityName)
        {
            EntityId = entityId;
            MappingId = mappingId;
            MappingValue = mappingValue;
            EntityName = entityName;
        }

        public int EntityId { get; private set; }

        public int MappingId { get; private set; }

        public string MappingValue { get; private set; }

        public string EntityName { get; private set; }
    }
}