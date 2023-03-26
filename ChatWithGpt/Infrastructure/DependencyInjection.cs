using Domain.Repositories;
using Infrastructure.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IChatHistoryRepository, ChatHistoryRepository>();
            return services;
        }
    }
}