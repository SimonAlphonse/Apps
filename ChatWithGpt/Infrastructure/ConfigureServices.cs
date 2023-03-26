using Domain.Repositories;
using Infrastructure.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection Infrastructure(this IServiceCollection services)
        {
            services.AddScoped<IChatHistoryRepository, ChatHistoryRepository>();
            return services;
        }
    }
}