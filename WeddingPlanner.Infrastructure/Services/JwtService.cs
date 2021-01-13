using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public JwtSecurityToken GenerateJwtToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[Constansts.Authentication.JwtSecretKeyConfig]));
            var token = new JwtSecurityToken(
                issuer: _config[Constansts.Authentication.JwtIssuerConfig],
                audience: _config[Constansts.Authentication.JwtAudienceConfig],
                claims: new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                },
                expires: DateTime.Now.AddDays(10),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
