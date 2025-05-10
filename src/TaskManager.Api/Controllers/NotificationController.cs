using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Repositories;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] int userId)
        {
            var unreadNotifications = await _notificationRepository.Find(x =>x.UserId == userId);

            return Ok(unreadNotifications.OrderByDescending(x=>x.CreatedAt));
        }

        [HttpPost("MarkAsRead/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var notification = await _notificationRepository.GetById(id);

            if (notification == null) return NotFound("Notification not found");

            notification.MarkAsRead();

            await _notificationRepository.Update(notification);

            return Ok();
        }
    }
}
