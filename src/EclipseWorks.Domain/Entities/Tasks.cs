using EclipseWorks.Domain.Enums;

namespace EclipseWorks.Domain.Entities
{
    public class Tasks : BaseEntity
    {
        public Tasks(int projectId, string title, string description, DateTime dueDate, TaskPriority priority)
        {
            Title = title;
            Description = description;
            ProjectId = projectId;
            DueDate = dueDate;
            Priority = priority;
            Status = TasksStatus.Pending;
        }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int ProjectId { get; private set; }
        public DateTime DueDate { get; private set; }
        public TaskPriority Priority { get; set; }
        public TasksStatus Status { get; private set; }
        public virtual Project? Project { get; private set; }
    }
}
