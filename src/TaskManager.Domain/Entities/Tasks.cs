﻿using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class Tasks : BaseEntity
    {
        public Tasks(int projectId, string title, string? description, DateTime dueDate, TaskPriority priority)
        {
            Title = title;
            Description = description;
            ProjectId = projectId;
            DueDate = dueDate;
            Priority = priority;
            Status = TasksStatus.Pending;
        }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public int ProjectId { get; private set; }
        public DateTime DueDate { get; private set; }
        public TaskPriority Priority { get; set; }
        public TasksStatus Status { get; private set; }
        public bool Delayed { get; private set; } = false;
        public virtual Project? Project { get; private set; }
        public virtual List<TaskHistory> Histories { get; private set; } = new List<TaskHistory>();
        public virtual List<TaskComment> Comments { get; private set; } = new List<TaskComment>();


        public void Update(string? description, TasksStatus status, string userName)
        {
            Description = description;
            Status = status;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsDelayed()
        {
            Delayed = true;
        }
    }
}
