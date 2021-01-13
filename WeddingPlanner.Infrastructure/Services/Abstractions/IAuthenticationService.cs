using System.Threading.Tasks;
using WeddingPlanner.Core.Services;
using WeddingPlanner.Infrastructure.Models.Authentication;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public interface IAuthenticationService : IService
    {
        Task<LoginResponse> AuthenticateAsync(LoginModel loginModel);
        Task<RegisterResponse> RegisterAsync(RegisterModel registerModel);
    }
}
