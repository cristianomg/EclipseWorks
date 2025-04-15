namespace EclipseWorks.Domain.Entities
{
    public class TaskHistoryChange : BaseEntity
    {
        public int HistoryId { get; set; }
        public string Field { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public virtual TaskHistory History { get; set; }
    }
}
