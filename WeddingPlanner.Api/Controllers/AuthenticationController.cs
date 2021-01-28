using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Models.Authentication;
using WeddingPlanner.Infrastructure.Services.Abstractions;

namespace WeddingPlanner.Api.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Authentication endpoint.
        /// </summary>
        /// <param name="user">User data - username and password (required).</param>
        /// <returns>JSON Web Token for given user data.</returns>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        [ProducesResponseType(typeof(LoginResponse), 401)]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            var loginResponse = await _authenticationService.AuthenticateAsync(user);
            if (!loginResponse.Result)
            {
                _logger.LogWarning("User not authorized.");
                return new UnauthorizedObjectResult(loginResponse);
            }

            _logger.LogInformation($"User {loginResponse.Item.Username} successfully logged in.");
            return new OkObjectResult(loginResponse);
        }

        /// <summary>
        /// New user registration endpoint.
        /// </summary>
        /// <param name="user">User data - username, email and password (all required).</param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(RegisterResponse), 200)]
        [ProducesResponseType(typeof(RegisterResponse), 400)]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            var registerResponse = await _authenticationService.RegisterAsync(user);
            if(!registerResponse.Result)
            {
                _logger.LogWarning("User not created.");
                return new UnauthorizedObjectResult(registerResponse);
            }

            _logger.LogInformation($"User {registerResponse.Item.Username} successfully logged in.");
            return new OkObjectResult(registerResponse);
        }
    }
}