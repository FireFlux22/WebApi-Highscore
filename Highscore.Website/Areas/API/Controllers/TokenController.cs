using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Highscore.Website.Areas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public TokenController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [HttpPost]
        public async Task<ActionResult> Create(Credentials credentials)
        {
            var user = await userManager.FindByNameAsync(credentials.UserName);

            var hasAccess = await userManager.CheckPasswordAsync(user, credentials.Password);

            if (!hasAccess)
            {
                return Unauthorized();
            }

            Token token = GenerateToken(user);

            return Ok(token);
        }

        private Token GenerateToken(IdentityUser user)
        {
            var signinKey = Convert.FromBase64String(configuration["Token:SigningKey"]);

            var expirationInMinutes = double.Parse(configuration["Token:ExpirationInMinutes"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // iat - Issued At Time
                IssuedAt = DateTime.UtcNow,

                // exp - Expiration Time
                Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),

                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim("email", user.Email)
                }),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(signinKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            var token = new Token
            {
                Value = jwtTokenHandler.WriteToken(jwtSecurityToken)
            };

            return token;
        }

        public class Token
        {
            public string Value { get; set; }

        }


        public class Credentials
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
