using Contracts.Models;
using Contracts.Repositories;
using Data.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Npgsql.NameTranslation;

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
            services.AddScoped<IGroupRepository>(x => new GroupRepository(x.GetService<DatabaseAccess>()));

            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            var a = NpgsqlConnection.GlobalTypeMapper.MapEnum<AccessModifier>("access_modifier",
                new NpgsqlNullNameTranslator());

            return services;
        }
    }
}