using System.Threading.Tasks;
using WeddingPlanner.Api.Models.Authentication;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Core.Services;

namespace WeddingPlanner.Api.Services.Abstractions
{
    public interface IAuthenticationService : IService
    {
        Task<LoginResponse> AuthenticateAsync(LoginModel loginModel);
        Task<RegisterResponse> RegisterAsync(RegisterModel registerModel);
    }
}
