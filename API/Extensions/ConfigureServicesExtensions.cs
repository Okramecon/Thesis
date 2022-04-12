using BLL.Services;
using Common.SettingsModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Linq;
using Model;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using DAL.Entities;
using DAL.EF;
using Microsoft.AspNetCore.Identity;
using API.Services;
using API.Infrastructure;

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
            services.AddScoped<UserService>();
            services.AddScoped<AuthService>();
            services.AddScoped<EmailService>();
            services.AddScoped<TokenService>();
            services.AddScoped<NewsService>();
            services.AddScoped<CurrentUserService>();
        }

        public static void AddCors(this IServiceCollection services, IConfiguration configuration)
        {
            var cors = configuration.GetSection("CorsSettings").Get<CorsSettingsModel>();
            services.AddCors(x => x.AddDefaultPolicy(b => b
                .WithOrigins(cors.Origins.ToArray())
                .AllowAnyMethod()
                .AllowAnyHeader()));
        }

        public static void RegisterJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var jwt = configuration.GetSection("JwtSettings").Get<AuthModel.JwtSettings>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                        ClockSkew = TimeSpan.Zero,
                    };
                });
        }

        public static void RegisterIOptions(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<AuthModel.JwtSettings>(x => configuration.GetSection("JwtSettings").Bind(x));
        }

        public static void RegisterAuth(this IServiceCollection services)
        {
            services.AddIdentityCore<User>(x =>
            {
                x.Password.RequireDigit = false;
                x.Password.RequiredLength = 6;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireUppercase = false;
            })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
