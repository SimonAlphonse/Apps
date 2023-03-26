using Application.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();
            return services;
        }

        public static IServiceProvider UsePersistence(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }
            return serviceProvider;
        }
    }
}