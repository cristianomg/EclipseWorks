using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManager.Application.Commands
{
    public class AddCommentCommand : IRequest
    {
        [JsonIgnore]
        public int TaskId { get; set; }
        [Required(ErrorMessage = "Comment can't be empty")]
        [MaxLength(500, ErrorMessage = "Comment can't be more than 500 caracters")]
        public string Comment { get; set; } = string.Empty;
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
