using MediatR;

namespace EclipseWorks.Application.Notifications
{
    public class UpdatedTaskNotification : INotification
    {
        public int TaskId { get; set; }
        public string UpdatedByUser { get; set; } = string.Empty;
        public Dictionary<string, string?> OldData { get; set; } = new Dictionary<string, string?>();
        public Dictionary<string, string?> NewData { get; set; } = new Dictionary<string, string?>();
        public int RetryCount { get; set; } = 0;
    }
}
