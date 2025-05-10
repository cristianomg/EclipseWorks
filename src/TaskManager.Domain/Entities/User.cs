using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(int id, string name, Role role)
        {
            Id = id;
            Name = name;
            Role = role;
        }
        public string Name { get; private set; }
        public Role Role { get; private set; }
        public virtual List<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
