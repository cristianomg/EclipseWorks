namespace EclipseWorks.Application.Handlers.Commands
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string? message) : base(message)
        {
        }
    }
}
