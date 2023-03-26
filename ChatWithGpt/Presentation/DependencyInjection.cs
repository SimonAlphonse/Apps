using Domain;

namespace Presenation.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresenation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddHttpClient(Constants.HttpClientFactory.OPEN_AI, client =>
            {
                client.BaseAddress = new Uri(Constants.OPEN_AI_URI);
            });

            return services;
        }

        public static WebApplication UsePresentation(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}