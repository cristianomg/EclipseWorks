using EclipseWorks.Domain.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace EclipseWorks.Application.Queries
{
    public class GetCompletedTasksCountByUserLast30DaysQuery : IRequest<GetCompletedTasksCountByUserLast30DaysQueryResponse>
    {
        [JsonIgnore]
        public int UserId { get; set; }
    }

    public class GetCompletedTasksCountByUserLast30DaysQueryResponse
    {
        public List<CompletedTasksPerUserDto> CompletedTasksPerUser { get; set; }
        public double Average { get; set; }
    }
}
