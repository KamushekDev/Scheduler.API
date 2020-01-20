using Microsoft.Extensions.DependencyInjection;
using Parser;

namespace API.Extensions
{
    public static class ParserExtensions
    {
        public static IServiceCollection AddParser(this IServiceCollection services)
        {
            services.AddScoped<LetiTimetableParser>();
            return services;
        }
    }
}