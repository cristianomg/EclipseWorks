namespace EclipseWorks.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Project(int userId, string name)
        {
            UserId = userId;
            Name = name;
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Tasks> Tasks { get; private set; } = Enumerable.Empty<Tasks>();    
        public virtual User? User { get; private set; }
    }
}
