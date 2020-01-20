using Contracts.Repositories;
using Data.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Dapper.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClassesRepository>(x => new ClassesRepository(x.GetService<DatabaseService>()));
            services.AddScoped<ITaskRepository>(x => new TaskRepository(x.GetService<DatabaseService>()));

            return services;
        }
    }
}