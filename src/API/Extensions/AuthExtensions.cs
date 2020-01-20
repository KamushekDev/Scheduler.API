using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var authSettings = configuration.GetSection("AuthSettings").Get<AuthSettings>();

            services.AddSingleton(authSettings);
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authSettings.TokenSettings.Issuer,
                        ValidAudience = authSettings.TokenSettings.Audience,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.TokenSettings.Key))
                    };
                })
                .AddVkontakte(options =>
                {
                    options.CallbackPath = "/api/auth/callback/vk";
                    options.ClientId = authSettings.VkSettings.ClientId.ToString();
                    options.ClientSecret = authSettings.VkSettings.ClientSecret;
                });
            
            return services;
        }
    }
}