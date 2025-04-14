using EclipseWorks.Domain.Enums;

namespace EclipseWorks.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string name, Role role)
        {
            Name = name;
            Role = role;
        }
        public string Name { get; private set; }
        public Role Role { get; private set; }
    }
}
