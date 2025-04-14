namespace EclipseWorks.Domain.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity() 
        {
            CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; protected set; }
    }
}
