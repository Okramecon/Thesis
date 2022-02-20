using BLL.Services;
using Common.SettingsModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

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

        public static void AddCors(this IServiceCollection services, IConfiguration configuration)
        {
            var cors = configuration.GetSection("CorsSettings").Get<CorsSettingsModel>();
            services.AddCors(x => x.AddDefaultPolicy(b => b
                .WithOrigins(cors.Origins.ToArray())
                .AllowAnyMethod()
                .AllowAnyHeader()));
        }
    }
}
