using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthSettings _authSettings;
        private readonly HttpClient _client = new HttpClient();

        public AuthController(AuthSettings authSettings)
        {
            _authSettings = authSettings;
        }

        [HttpGet("vk")]
        public IActionResult LoginWithVkontakte()
        {
            return Challenge(new AuthenticationProperties() {AllowRefresh = true}, "Vkontakte");
        }

        [HttpGet("callback/vk")]
        public async Task<IActionResult> VkontakteCallback([FromQuery] string code, [FromQuery] string state)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"client_id", _authSettings.VkSettings.ClientId.ToString()},
                {"client_secret", _authSettings.VkSettings.ClientSecret},
                {"redirect_uri", _authSettings.VkSettings.RedirectUri},
                {"code", code}
            };
            var query = QueryHelpers.AddQueryString("https://oauth.vk.com/access_token", queryParams);

            var response = await _client.GetStringAsync(query);

            var result = JsonSerializer.Deserialize<VkAccessTokenResponse>(response);

            //todo: Save result or smth

            return Redirect($"/auth/{GenerateNewToken("vk", result.UserId.ToString())}");
        }

        private string GenerateNewToken(string provider, string userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.TokenSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = GetIdentity(provider, userId);

            var token = new JwtSecurityToken(_authSettings.TokenSettings.Issuer, _authSettings.TokenSettings.Audience,
                // тут можно навесить какие угодно claims о пользователе
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> GetIdentity(string provider, string userId)
        {
            var list = new List<Claim>();

            list.Add(new Claim("Roles", "JustForBuid"));
            list.Add(new Claim(nameof(provider), provider));
            list.Add(new Claim(nameof(userId), userId));

            return list;
        }
    }
}