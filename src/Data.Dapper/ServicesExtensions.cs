using Contracts.Repositories;
using Data.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Dapper
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository>(x => new UserRepository());
            services.AddScoped<IClassesRepository>(x => new ClassesRepository(x.GetService<BaseDataAcсess>()));


            return services;
        }
    }
}