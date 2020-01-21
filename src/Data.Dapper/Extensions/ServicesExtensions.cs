using Contracts.Repositories;
using Data.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Dapper.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository>(x => new UserRepository(x.GetService<DatabaseAccess>()));
            services.AddScoped<IClassesRepository>(x => new ClassesRepository(x.GetService<DatabaseAccess>()));
            services.AddScoped<ITaskRepository>(x => new TaskRepository(x.GetService<DatabaseAccess>()));
            services.AddScoped<ICredentialRepository>(x => new CredentialRepository(x.GetService<DatabaseAccess>()));

            return services;
        }
    }
}