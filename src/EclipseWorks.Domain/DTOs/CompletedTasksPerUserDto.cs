using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseWorks.Domain.DTOs
{
    public class CompletedTasksPerUserDto
    {
        public string UserName { get; set; }
        public int Count { get; set; }
    }
}
