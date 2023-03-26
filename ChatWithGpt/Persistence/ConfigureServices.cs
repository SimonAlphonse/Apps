using Application.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();
            return services;
        }

        public static void ConfigureDatabase(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}