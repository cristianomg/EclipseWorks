using EclipseWorks.Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EclipseWorks.Application.Commands
{
    public record UpdateTaskCommand : IRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Task id is required.")]
        public int TaskId { get; set; }
        public TasksStatus? Status { get; set; }
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
