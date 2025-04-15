using EclipseWorks.Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EclipseWorks.Application.Commands
{
    public record UpdateTaskCommand : IRequest
    {
        public TasksStatus? Status { get; set; }
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public int TaskId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
