namespace EclipseWorks.Domain.Exceptions
{
    public class ProjectNameInUseException : Exception
    {
        public ProjectNameInUseException(string projectName) : base($"A project with the name {projectName} already exists.")
        {
        }
    }
}
