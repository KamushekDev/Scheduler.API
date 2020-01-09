using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JWToken _token;

        public AuthController(JWToken token)
        {
            _token = token;
        }

        // [HttpPost("login")]
        // public IActionResult Login([FromBody] string userName)
        // {
        //     // тут вместо этой хуйни нормальная проверка логина/пасса 
        //     if (userName == null)
        //     {
        //         return Unauthorized();
        //     }
        //
        //     return Ok(new {token = GenerateNewToken(userName)});
        // }

        // [HttpGet("github")]
        // public IActionResult LoginWithGutHub()
        // {
        //     return Challenge(new AuthenticationProperties() {AllowRefresh = true}, "GitHub");
        // }

        [HttpGet("vk")]
        public IActionResult LoginWithVkontakte()
        {
            return Challenge(new AuthenticationProperties() {AllowRefresh = true}, "Vkontakte");
        }

        [HttpGet("callback/vk")]
        public async Task<IActionResult> VkontakteCallback([FromQuery] string code, [FromQuery] string state)
        {
            var client = new HttpClient();

            var clientSecret = "CgRa3MRXf5CF2ttWutPl";

            var redirectUri = "http://31.134.151.83/api/auth/callback/vk";

            var clientId = 7263896;

            var response =
                await client.GetAsync(
                    $"https://oauth.vk.com/access_token?client_id={clientId}&client_secret={clientSecret}&redirect_uri={redirectUri}&code={code}");

            var res = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<VKResponse>(res);

            //Save result or smth

            return Redirect($"/auth/{GenerateNewToken("vk", result.user_id.ToString())}");
        }

        public class VKResponse
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public int user_id { get; set; }
        }

        [HttpGet("callback/github")]
        // public IActionResult GitHubCallback([FromQuery] string client_id, [FromQuery] string scope, [FromQuery] string response_type, [FromQuery] string redirect_uri, [FromQuery] string state)
        public async Task<IActionResult> GitHubCallback([FromQuery] string code, [FromQuery] string state)
        {
            // Console.WriteLine($"CODE: {access_token}");
            // return Ok($"code: {access_token}");

            var client = new HttpClient();
            try
            {
                var response = await client.GetAsync($"https://api.github.com/user?access_token={code}",
                    new CancellationTokenSource(1000).Token);


                return Redirect("/");
                return Redirect("/auth/callback?name");
            }
            catch (TaskCanceledException e)
            {
                Console.WriteLine(e);
            }

            return Redirect("/error");
        }

        private string GenerateNewToken(string provider, string userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = GetIdentity(provider, userId);

            var token = new JwtSecurityToken(_token.Issuer, _token.Audience,
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