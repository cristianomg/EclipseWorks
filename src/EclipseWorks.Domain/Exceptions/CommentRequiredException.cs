using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseWorks.Domain.Exceptions
{
    public class CommentRequiredException : Exception
    {
        public CommentRequiredException(string? message) : base(message)
        {
        }
    }
}
