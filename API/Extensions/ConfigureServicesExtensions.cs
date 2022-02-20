using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<DepartmentService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<BoardService>();
            services.AddScoped<UserStoryService>();
            services.AddScoped<TicketService>();
        }
    }
}
