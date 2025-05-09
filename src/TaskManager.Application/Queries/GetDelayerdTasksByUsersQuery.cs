using MediatR;
using TaskManager.Domain.DTOs;

namespace TaskManager.Application.Queries
{
    public class GetDelayerdTasksByUsersQuery : IRequest<GetDelayerdTasksByUsersQueryResponse>
    {
        public int UserId { get; set; }
    }
    public class GetDelayerdTasksByUsersQueryResponse 
    {
        public List<DelayedTasksByUser> DelayedTasksByUsers { get; set; }
        public double Average { get; set; }
    }


}
