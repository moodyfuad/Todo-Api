using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistant;
using Persistant.Identity;
using Persistant.Repositories;
using Service.Abstraction;
using Services;
using Services.JwtServices;
using Shared.Helpers;
using System.Text;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationRequiredServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Persistant 
            services.AddDataAccess(configuration);
            // jwt
            services.AddJwtConfigurations(configuration);
            // services
            services.AddScoped<IServiceManager, ServiceManager>();


            return services;

        }
        private static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Local") ??
                throw new InvalidOperationException("Database Connection String is not configured.");

            // Repository 
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            //database connection.
            services.AddDbContext<RepositoryDbContext>(options =>
            options.UseSqlServer(connectionString)
            );

            //todo: adding the identity tables to the database.
            //services.AddIdentity<AppUser, IdentityRole<Guid>>(op =>
            //{
            //    op.Password.RequiredLength = 4;
            //    op.Password.RequireDigit = false;
            //    op.Password.RequireNonAlphanumeric = false;
            //    op.Password.RequiredUniqueChars = 0;
            //    op.Password.RequireUppercase = false;
            //}).
            //    AddEntityFrameworkStores<RepositoryDbContext>().
            //    AddDefaultTokenProviders();

            return services;
        }
        private static IServiceCollection AddJwtConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });
            services.AddAuthorization();

            return services;
        }


    }
}
