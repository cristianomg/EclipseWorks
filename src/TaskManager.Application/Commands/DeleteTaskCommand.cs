using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManager.Application.Commands
{
    public record DeleteTaskCommand : IRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Task id is required.")]
        public int TaskId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
