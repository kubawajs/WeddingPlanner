using System.IdentityModel.Tokens.Jwt;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Core.Services;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public interface IJwtService : IService
    {
        JwtSecurityToken GenerateJwtToken(User userInfo);
    }
}
