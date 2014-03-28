namespace Common.Events
{
    public class EntitySelectedEvent
    {
        public EntitySelectedEvent(string entityKey, int id, string entityValue)
        {
            this.EntityKey = entityKey;
            this.Id = id;
            this.EntityValue = entityValue;
        }

        public string EntityKey { get; set; }

        public string EntityValue { get; set; }

        public int Id { get; set; }
    }
}