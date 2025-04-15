namespace EclipseWorks.Domain.Entities
{
    public class TaskHistory : BaseEntity
    {
        public TaskHistory(int taskId, string updatedByUser) 
        {
            TaskId = taskId;
            UpdatedByUser = updatedByUser;    
        }
        public int TaskId { get; private set; }
        public string UpdatedByUser { get; private set; }
        public virtual Tasks? Task { get; private set; }
        public virtual List<TaskHistoryChange> Changes { get; private set; } = new List<TaskHistoryChange>();
    }
}
