using System.IdentityModel.Tokens.Jwt;
using WeddingPlanner.Core.Services;
using WeddingPlanner.Infrastructure.Models;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public interface IJwtService : IService
    {
        JwtSecurityToken GenerateJwtToken(User userInfo);
    }
}
