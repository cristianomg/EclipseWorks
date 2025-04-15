using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EclipseWorks.Application.Commands
{
    public record DeleteProjectCommand : IRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Project id is required.")]
        public int ProjectId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
