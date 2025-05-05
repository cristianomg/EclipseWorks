namespace TaskManager.Domain.Exceptions
{
    public class TaskLimitExceededException : Exception
    {
        public TaskLimitExceededException(int maxTasks) : base($"The project cannot have more than {maxTasks} tasks.")
        {
        }
    }
}
