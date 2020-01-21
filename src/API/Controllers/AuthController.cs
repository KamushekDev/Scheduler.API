using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;
using Contracts.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthSettings _authSettings;

        public AuthController(AuthSettings authSettings)
        {
            _authSettings = authSettings;
        }

        [HttpGet("vk")]
        public IActionResult LoginWithVkontakte()
        {
            return Challenge(new AuthenticationProperties() {AllowRefresh = true},
                _authSettings.VkSettings.ProviderName);
        }

        [HttpGet("callback/vk")]
        public async Task<IActionResult> VkontakteCallback([FromQuery] string code, [FromQuery] string state,
            [FromServices] ICredentialRepository credentialRepository, [FromServices] IUserRepository userRepository)
        {
            var codeParams = new Dictionary<string, string>
            {
                {"client_id", _authSettings.VkSettings.ClientId.ToString()},
                {"client_secret", _authSettings.VkSettings.ClientSecret},
                {"redirect_uri", _authSettings.VkSettings.RedirectUri},
                {"code", code}
            };
            var accessTokenQuery = QueryHelpers.AddQueryString("https://oauth.vk.com/access_token", codeParams);

            var client = new HttpClient();
            var response = await client.GetStringAsync(accessTokenQuery);
            var accessTokenResponse = JsonSerializer.Deserialize<VkAccessTokenResponse>(response);

            //-------------------

            var credentials = await credentialRepository.GetByProvider(_authSettings.VkSettings.ProviderName,
                accessTokenResponse.UserId);

            if (credentials == null)
            {
                var profileParams = new Dictionary<string, string>
                {
                    {"access_token", accessTokenResponse.AccessToken},
                    {"v", "5.103"},
                    {"user_ids", accessTokenResponse.UserId.ToString()}
                };

                var profileQuery = QueryHelpers.AddQueryString("https://api.vk.com/method/users.get", profileParams);

                var profileResponse = await client.GetStringAsync(profileQuery);
                
                var profile = JsonSerializer.Deserialize<VkGetResponse>(profileResponse).Response.First();

                var userId = await userRepository.AddUser(profile.FirstName, profile.LastName);

                var res = await credentialRepository.AddCredential(
                    _authSettings.VkSettings.ProviderName,
                    accessTokenResponse.UserId,
                    userId,
                    accessTokenResponse.AccessToken,
                    DateTime.Now.AddSeconds(accessTokenResponse.ExpiresIn));

                credentials = await credentialRepository.GetByProvider(_authSettings.VkSettings.ProviderName,
                    accessTokenResponse.UserId);
            }


            //todo: Save results to db

            return Redirect($"/auth/{GenerateNewToken(_authSettings.VkSettings.ProviderName, accessTokenResponse.UserId, credentials.UserId)}");
        }

        private string GenerateNewToken(string provider, int providerUserId, int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.TokenSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = GetIdentity(provider, userId, userId);

            var token = new JwtSecurityToken(_authSettings.TokenSettings.Issuer, _authSettings.TokenSettings.Audience,
                claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> GetIdentity(string provider, int providerUserId, int userId)
        {
            var list = new List<Claim>();

            // тут можно навесить какие угодно claims о пользователе
            list.Add(new Claim("Roles", "JustForBuild"));
            list.Add(new Claim(nameof(provider), provider));
            list.Add(new Claim(nameof(providerUserId), providerUserId.ToString()));
            list.Add(new Claim(nameof(userId), userId.ToString()));

            return list;
        }
    }
}