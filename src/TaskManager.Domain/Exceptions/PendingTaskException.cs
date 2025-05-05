namespace TaskManager.Domain.Exceptions
{
    public class PendingTaskException : Exception
    {
        public PendingTaskException(string? message) : base(message)
        {
        }
    }
}
