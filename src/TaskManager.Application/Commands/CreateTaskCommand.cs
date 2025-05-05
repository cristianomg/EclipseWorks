using TaskManager.Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskManager.Application.CustomValidationAttributes;

namespace TaskManager.Application.Commands
{
    public record CreateTaskCommand : IRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Project id is required.")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Title can't be empty.")]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required(ErrorMessage = "Priority can't be empty.")]
        [EnumDataType(typeof(TaskPriority), ErrorMessage = "The selected task priority is invalid.")]
        public TaskPriority Priority { get; set; }
        [Required(ErrorMessage = "Due date can't be empty.")]
        [YearGreaterThan(2020)]
        public DateTime DueDate { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
