using Domain.Managers;

using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IChatManager, ChatManager>();
            return services;
        }
    }
}