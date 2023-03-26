using Domain;

namespace Presenation.Extensions.DependencyInjection
{
    internal static class ConfigureServices
    {
        public static IServiceCollection AddPresenation(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddHttpClient(Constants.HttpClientFactory.OPEN_AI, client =>
            {
                client.BaseAddress = new Uri(Constants.OPEN_AI_URI);
            });

            return services;
        }
    }
}