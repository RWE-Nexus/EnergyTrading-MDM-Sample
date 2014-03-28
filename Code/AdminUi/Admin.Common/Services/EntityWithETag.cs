namespace Common.Services
{
    public class EntityWithETag<T>
    {
        public EntityWithETag(T entity, string etag)
        {
            this.ETag = etag;
            this.Object = entity;
        }

        public string ETag { get; set; }

        public T Object { get; set; }
    }
}