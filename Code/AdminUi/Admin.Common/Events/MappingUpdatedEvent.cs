namespace Common.Events
{
    using System;

    public class MappingUpdatedEvent
    {
        public MappingUpdatedEvent(int entityId, int mappingId, string newValue, DateTime startDate)
        {
            this.EntityId = entityId;
            this.MappingId = mappingId;
            this.NewValue = newValue;
            this.StartDate = startDate;
        }

        public MappingUpdatedEvent(
            int entityId, 
            int mappingId, 
            string newValue, 
            DateTime startDate, 
            bool isDefault, 
            bool isSourceSystemOriginated)
        {
            this.EntityId = entityId;
            this.MappingId = mappingId;
            this.NewValue = newValue;
            this.StartDate = startDate;
            this.IsDefault = isDefault;
            this.IsSourceSystemOriginated = isSourceSystemOriginated;
        }

        public bool Cancelled { get; private set; }

        public int EntityId { get; private set; }

        public bool IsDefault { get; private set; }

        public bool IsSourceSystemOriginated { get; private set; }

        public int MappingId { get; private set; }

        public string NewValue { get; private set; }

        public DateTime StartDate { get; private set; }

        public static MappingUpdatedEvent ForCancellation()
        {
            return new MappingUpdatedEvent(0, 0, string.Empty, DateTime.MinValue) { Cancelled = true };
        }
    }
}