namespace EclipseWorks.Domain.Entities
{
    public class TaskHistoryChange : BaseEntity
    {
        public TaskHistoryChange(string field, string? oldValue, string newValue)
        {
            Field = field;
            OldValue = oldValue;
            NewValue = newValue;
        }
        public int HistoryId { get; private set; }
        public string Field { get; private set; }
        public string? OldValue { get; private set; }
        public string NewValue { get; private set; }
        public virtual TaskHistory? History { get; private set; }
    }
}
