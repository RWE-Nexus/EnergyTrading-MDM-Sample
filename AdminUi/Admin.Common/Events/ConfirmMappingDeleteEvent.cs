namespace Common.Events
{
    public class ConfirmMappingDeleteEvent
    {
        public ConfirmMappingDeleteEvent(int mappingId, string mappingValue, string systemName)
        {
            MappingId = mappingId;
            MappingValue = mappingValue;
            SystemName = systemName;
        }

        public int MappingId { get; private set; }

        public string MappingValue { get; private set; }

        public string SystemName { get; private set; }
    }
}