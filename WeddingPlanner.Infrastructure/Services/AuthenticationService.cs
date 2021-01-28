using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto;
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
        private readonly IMapper _mapper;

        public AuthenticationService(
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IJwtService jwtService,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<LoginResponse> AuthenticateAsync(LoginModel loginModel)
        {
            if(loginModel == null)
            {
                return new LoginResponse(
                    BaseApiResponse<UserDto>.CreateErrorResponse("LoginModel cannot be null."));
            }

            try
            {
                var user = await _userManager.FindByNameAsync(loginModel.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    // TODO: roles
                    // var userRoles = await _userManager.GetRolesAsync(user);

                    var token = _jwtService.GenerateJwtToken(user);
                    var userDto = _mapper.Map<UserDto>(user);
                    var response = new LoginResponse(
                        BaseApiResponse<UserDto>.CreateSuccessResponse("User authenticated successfully!", userDto))
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo
                    };

                    return response;
                }

                return new LoginResponse(BaseApiResponse<UserDto>.CreateErrorResponse("User not authenticated."));
            }
            catch (Exception ex)
            {
                return new LoginResponse(BaseApiResponse<UserDto>.CreateErrorResponse(
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
                    return new RegisterResponse(BaseApiResponse<UserDto>.CreateErrorResponse($"User already exists!"));
                }

                var user = new User(registerModel);
                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (!result.Succeeded)
                {
                    return new RegisterResponse(BaseApiResponse<UserDto>.CreateErrorResponse($"User creation failed: {result.Errors.GetErrorsAsString()}"));
                }

                var userDto = _mapper.Map<UserDto>(user);
                return new RegisterResponse(
                    BaseApiResponse<UserDto>.CreateSuccessResponse("User created successfully!", userDto));
            }
            catch (Exception ex)
            {
                return new RegisterResponse(BaseApiResponse<UserDto>.CreateErrorResponse(
                    $"An error occured during user registration: {ex.Message}"));
            }
        }
    }
}