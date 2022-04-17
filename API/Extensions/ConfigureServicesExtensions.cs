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
using Microsoft.OpenApi.Models;
using Thesis;
using System.IO;
using System.Reflection;

namespace API.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<DepartmentService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<BoardService>();
            services.AddScoped<TicketService>();
            services.AddScoped<UserService>();
            services.AddScoped<AuthService>();
            services.AddScoped<EmailService>();
            services.AddScoped<TokenService>();
            services.AddScoped<NewsService>();
            services.AddScoped<CurrentUserService>();
            services.AddScoped<CommentService>();
            services.AddScoped<MediaService>();
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

        public static void RegisterSwagger(this IServiceCollection services)
        {
            var xmlFileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
            var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Thesis" });

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                x.CustomSchemaIds(t => t.FullName);

                //x.IncludeXmlComments(xmlFilePath, true);
            });
        }
    }
}
