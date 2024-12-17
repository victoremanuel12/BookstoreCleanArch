using Application.Behaviors;
using Application.Mappings;
using Application.ServiceInterface;
using Application.Services;
using Domain.Interfaces;
using FluentValidation;
using Identity.Data;
using Identity.Services;
using Infra.Data;
using Infra.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.AddDbContext<IdentityDataContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });



            services.AddAutoMapper(typeof(Mappings));

            services.AddValidatorsFromAssembly(Assembly.Load("Application"));
            services.AddMediatR(cfg =>
            {
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.RegisterServicesFromAssembly(Assembly.Load("Application"));
                cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<IdentityDataContext>()
              .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthorService, AuthorService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            return services;
        }
    }

}
