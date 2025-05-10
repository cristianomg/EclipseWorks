using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.Application.Commands;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;


namespace TaskManager.Application.Handlers.Commands
{
    public class AddNotificationCommandHandler : IRequestHandler<AddNotificationCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddNotificationCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            foreach(var userId in request.Users)
            {
                var user = await _userRepository.GetById(userId);

                if (user == null)
                {
                    continue;
                }

                user.Notifications.Add(new Notification(request.Message));

                await _userRepository.Update(user);
            }
        }
    }
}
