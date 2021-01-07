using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Extensions;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Models.Authentication;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AuthenticationService(
            IConfiguration config,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            _config = config;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<LoginResponse> Authenticate(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);  
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))  
            {  
                // TODO: roles
                // var userRoles = await _userManager.GetRolesAsync(user);

                var token = GenerateJwtToken(user);
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

        public async Task<RegisterResponse> Register(RegisterModel registerModel)
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

        private JwtSecurityToken GenerateJwtToken(User userInfo)
        {
            // TODO: to constants
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
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