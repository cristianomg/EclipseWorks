namespace EclipseWorks.Domain.Entities
{
    public class TaskComment : BaseEntity
    {
        public TaskComment(string value)
        {
            Value = value;  
        }
        public string Value { get; private set; } 
    }
}
