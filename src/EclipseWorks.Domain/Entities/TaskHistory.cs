namespace EclipseWorks.Domain.Entities
{
    public class TaskHistory : BaseEntity
    {
        public int TaskId { get; set; }
        public string UpdatedByUser { get; set; }
        public virtual Tasks Task { get; set; }
        public virtual List<TaskHistoryChange> Changes { get; set; }
    }
}
