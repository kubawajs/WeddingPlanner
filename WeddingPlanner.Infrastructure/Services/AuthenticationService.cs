using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Extensions;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Models.Abstractions;
using WeddingPlanner.Infrastructure.Models.Authentication;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;

        public AuthenticationService(
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IJwtService jwtService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse> AuthenticateAsync(LoginModel loginModel)
        {
            if(loginModel == null)
            {
                return new LoginResponse(BaseApiResponse.CreateErrorResponse("LoginModel cannot be null."));
            }

            try
            {
                var user = await _userManager.FindByNameAsync(loginModel.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    // TODO: roles
                    // var userRoles = await _userManager.GetRolesAsync(user);

                    var token = _jwtService.GenerateJwtToken(user);
                    var response = new LoginResponse(BaseApiResponse.CreateSuccessResponse("User authenticated successfully!"))
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Username = user.UserName,
                        Expiration = token.ValidTo
                    };

                    return response;
                }

                return new LoginResponse(BaseApiResponse.CreateErrorResponse("User not authenticated."));
            }
            catch (Exception ex)
            {
                return new LoginResponse(BaseApiResponse.CreateErrorResponse(
                    $"An error occured during user authentication: {ex.Message}"));
            }
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterModel registerModel)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(registerModel.Username);
                if (userExists != null)
                {
                    return new RegisterResponse(BaseApiResponse.CreateErrorResponse($"User already exists!"));
                }

                var user = new User(registerModel);
                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (!result.Succeeded)
                {
                    return new RegisterResponse(BaseApiResponse.CreateErrorResponse($"User creation failed: {result.Errors.GetErrorsAsString()}"));
                }

                var response = new RegisterResponse(BaseApiResponse.CreateSuccessResponse("User created successfully!"))
                {
                    Username = registerModel.Username
                };
                return response;
            }
            catch (Exception ex)
            {
                return new RegisterResponse(BaseApiResponse.CreateErrorResponse(
                    $"An error occured during user registration: {ex.Message}"));
            }
        }
    }
}