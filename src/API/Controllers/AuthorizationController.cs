using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Contracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly JWToken _token;
        
        public AuthorizationController(JWToken token)
        {
            _token = token;
        }
        
        [AllowAnonymous] 
        [HttpPost("login")]
        public IActionResult Login([FromBody] string userName)
        {
            // тут вместо этой хуйни нормальная проверка логина/пасса 
            if (userName == null)
            {
                return Unauthorized();
            }
            
            return Ok(new {token = GenerateNewToken(userName)});
        }

        public string GenerateNewToken(string userName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = GetIdentity(userName);

            var token = new JwtSecurityToken(_token.Issuer, _token.Audience, 
                // тут можно навесить какие угодно claims о пользователе
                claims,
                expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> GetIdentity(string userName)
        {
            if (userName == "TestUser")
            {
                return new List<Claim>{new Claim("Roles", "JustForBuid")};
            }

            return null;
        }
    }
}