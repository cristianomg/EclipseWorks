using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EclipseWorks.Application.Commands.Project
{
    public record CreateProjectCommand : IRequest
    {
        [Required(ErrorMessage = "Project name can't be empty.")]
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
