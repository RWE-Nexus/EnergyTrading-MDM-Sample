namespace Common.Events
{
    public class EntitySelectEvent
    {
        public EntitySelectEvent(string entityName) : this(entityName, entityName)
        {
        }

        public EntitySelectEvent(string entityName, string propertyName)
        {
            this.EntityName = entityName;
            this.PropertyName = propertyName;
        }

        public string EntityName { get; set; }

        public string PropertyName { get; set; }
    }
}