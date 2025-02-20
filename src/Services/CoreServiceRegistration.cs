using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityProvider.Services
{
    public static class CoreServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddTransient<ISmsService, SmsService>();

            return services;
        }
    }
}
