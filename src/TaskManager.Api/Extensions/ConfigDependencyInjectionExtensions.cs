using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Workers;
using TaskManager.Domain.Repositories;
using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Api.Extensions
{
    public static class ConfigDependencyInjectionExtensions
    {
        public static IServiceCollection AddConfigurationDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Failed to load the database connection string.");
            Console.WriteLine(connection);
            services.AddDbContext<DataContext>(options => options.UseNpgsql(connection));
            services.AddMediatR(c => c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.Load("TaskManager.Application")));
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "http://localhost:5000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskHistoryRepository, TaskHistoryRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            services.AddHostedService<CheckTaskDueDateWorker>();

            return services;
        }
    }
}
