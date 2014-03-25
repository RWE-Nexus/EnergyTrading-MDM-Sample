namespace Common.EntitySelector
{
    using System;

    public class EntityIdViewModel
    {
        public EntityIdViewModel(int id, string name, Type entityType)
        {
            this.Id = id;
            this.Name = name;
            this.EntityType = entityType;
        }

        public Type EntityType { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}