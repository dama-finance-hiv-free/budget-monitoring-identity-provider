using IdentityProvider.Data.Base;
using IdentityProvider.Data.Repository.MultiFactorType;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityProvider.Data
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseFactory, DatabaseFactory>();
            services.AddScoped<IMultiFactorTypePersistence, MultiFactorTypePersistence>();

            return services;
        }
    }
}
