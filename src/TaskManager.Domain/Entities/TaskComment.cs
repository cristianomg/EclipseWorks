namespace TaskManager.Domain.Entities
{
    public class TaskComment : BaseEntity
    {
        public TaskComment(string value)
        {
            Value = value;
        }
        public int TaskId { get; private set; }
        public string Value { get; private set; }
    }
}
