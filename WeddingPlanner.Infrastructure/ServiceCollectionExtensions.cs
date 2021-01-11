using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WeddingPlanner.Infrastructure.Repository;
using WeddingPlanner.Infrastructure.Repository.Abstractions;
using WeddingPlanner.Infrastructure.Services;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeddingPlannerInfrastructure(this IServiceCollection services, IMapper mapper)
        {
            // Add Automapper
            services.AddSingleton(mapper);

            // Services
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IWeddingHallService, WeddingHallService>();

            // Repositories
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IWeddingHallRespository, WeddingHallRepository>();

            return services;
        }
    }
}
