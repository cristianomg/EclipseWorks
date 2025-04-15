using EclipseWorks.Domain.Repositories;
using EclipseWorks.Infrastructure;
using EclipseWorks.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EclipseWorks.Api.Extensions
{
    public static class ConfigDependencyInjectionExtensions
    {
        public static IServiceCollection AddConfigurationDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Failed to load the database connection string.");
            Console.WriteLine(connection);
            services.AddDbContext<DataContext>(options => options.UseNpgsql(connection));
            services.AddMediatR(c => c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.Load("EclipseWorks.Application")));
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
