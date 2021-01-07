using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Models.Authentication;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> Authenticate(LoginModel loginModel);
        Task<RegisterResponse> Register(RegisterModel registerModel);
    }
}
