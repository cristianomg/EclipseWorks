using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Commands
{
    public class AddNotificationCommand : IRequest
    {
        public int[] Users { get; set; }
        public string Message { get; set; }
        public string RedirectUrl { get; set; }
    }
}
