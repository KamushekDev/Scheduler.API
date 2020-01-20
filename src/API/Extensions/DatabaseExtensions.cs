using Contracts.Helpers;
using Contracts.Repositories;
using Data.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionString").Get<string>();

            var databaseConfiguration = new DatabaseConfiguration(connectionString);

            services.AddSingleton(databaseConfiguration);
            services.AddScoped<BaseDataAcсess>();

            services.AddRepositories();
            
            return services;
        }
    }
}