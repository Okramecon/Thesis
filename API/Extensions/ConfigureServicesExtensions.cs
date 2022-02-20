using BLL.Services;
using DAL.EF;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<DepartmentService>();
        }
    }
}
