using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Models.Abstractions;
using WeddingPlanner.Infrastructure.Models.Authentication;
using WeddingPlanner.Infrastructure.Services;
using WeddingPlanner.Infrastructure.Services.Abstractions;
using Xunit;

namespace WeddingPlanner.Tests.Services
{
    public class AuthenticationServiceUnitTests
    {
        [Fact]
        public async Task Authenticate_ReturnSuccessResponse_ForValidLoginModel()
        {
            // Arrange
            var mockUser = new User
            {
                UserName = "Test"
            };

            var mockUserDto = new UserDto
            {
                Username = "Test"
            };

            var mockLoginModel = new LoginModel
            {
                Username = "Test",
                Password = "Test123$"
            };

            var mockJwtService = new Mock<IJwtService>();
            mockJwtService.Setup(x => x.GenerateJwtToken(mockUser)).Returns(new JwtSecurityToken());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<UserDto>(mockUser)).Returns(mockUserDto);

            var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
            var mockRoleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(x => x.FindByNameAsync(mockLoginModel.Username)).ReturnsAsync(mockUser);
            mockUserManager.Setup(x => x.CheckPasswordAsync(mockUser, mockLoginModel.Password)).ReturnsAsync(true);

            // Act
            var service = new AuthenticationService(
                mockRoleManager.Object,
                mockUserManager.Object,
                mockJwtService.Object,
                mockMapper.Object);
            var result = await service.AuthenticateAsync(mockLoginModel);

            // Assert
            result.Should().BeOfType<LoginResponse>();
            result.Result.Should().BeTrue();
            result.Status.Should().Be(ResponseStatus.Success);
            result.Item.Username.Should().Be(mockLoginModel.Username);
        }

        [Fact]
        public async Task Authenticate_ReturnErrorResponse_ForNullParam()
        {
            // Arrange
            LoginModel mockLoginModel = null;
            var mockUser = new User
            {
                UserName = "Test"
            };

            var mockUserDto = new UserDto
            {
                Username = "Test"
            };

            var mockJwtService = new Mock<IJwtService>();
            mockJwtService.Setup(x => x.GenerateJwtToken(mockUser)).Returns(new JwtSecurityToken());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<UserDto>(mockUser)).Returns(mockUserDto);

            var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
            var mockRoleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            // Act
            var service = new AuthenticationService(
                mockRoleManager.Object,
                mockUserManager.Object, 
                mockJwtService.Object,
                mockMapper.Object);
            var result = await service.AuthenticateAsync(mockLoginModel);

            // Assert
            result.Should().BeOfType<LoginResponse>();
            result.Result.Should().BeFalse();
            result.Status.Should().Be(ResponseStatus.Error);
            result.Item.Should().BeNull();
            result.Token.Should().BeNull();
        }

        [Fact]
        public async Task Authenticate_ReturnErrorResponse_IfExceptionThrown()
        {
            // Arrange
            var mockUser = new User
            {
                UserName = "Test"
            };

            var mockUserDto = new UserDto
            {
                Username = "Test"
            };

            var mockLoginModel = new LoginModel
            {
                Username = "Test",
                Password = "Test123$"
            };

            var mockJwtService = new Mock<IJwtService>();
            mockJwtService.Setup(x => x.GenerateJwtToken(mockUser)).Returns(new JwtSecurityToken());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<UserDto>(mockUser)).Returns(mockUserDto);

            var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
            var mockRoleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(x => x.FindByNameAsync(mockLoginModel.Username)).Throws(new Exception("test exception"));
            mockUserManager.Setup(x => x.CheckPasswordAsync(mockUser, mockLoginModel.Password)).ReturnsAsync(true);

            // Act
            var service = new AuthenticationService(
                mockRoleManager.Object,
                mockUserManager.Object, 
                mockJwtService.Object,
                mockMapper.Object);
            var result = await service.AuthenticateAsync(mockLoginModel);

            // Assert
            result.Should().BeOfType<LoginResponse>();
            result.Result.Should().BeFalse();
            result.Status.Should().Be(ResponseStatus.Error);
            result.Item.Should().BeNull();
            result.Token.Should().BeNull();
        }

        [Fact]
        public async Task Authenticate_ReturnErrorResponse_ForWrongPassword()
        {
            // Arrange
            var mockUser = new User
            {
                UserName = "Test"
            };

            var mockUserDto = new UserDto
            {
                Username = "Test"
            };

            var mockLoginModel = new LoginModel
            {
                Username = "Test",
                Password = "Test123$"
            };

            var mockJwtService = new Mock<IJwtService>();
            mockJwtService.Setup(x => x.GenerateJwtToken(mockUser)).Returns(new JwtSecurityToken());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<UserDto>(mockUser)).Returns(mockUserDto);

            var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
            var mockRoleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(x => x.FindByNameAsync(mockLoginModel.Username)).ReturnsAsync(mockUser);
            mockUserManager.Setup(x => x.CheckPasswordAsync(mockUser, mockLoginModel.Password)).ReturnsAsync(false);

            // Act
            var service = new AuthenticationService(
                mockRoleManager.Object,
                mockUserManager.Object,
                mockJwtService.Object,
                mockMapper.Object);
            var result = await service.AuthenticateAsync(mockLoginModel);

            // Assert
            result.Should().BeOfType<LoginResponse>();
            result.Result.Should().BeFalse();
            result.Status.Should().Be(ResponseStatus.Error);
            result.Item.Should().BeNull();
            result.Token.Should().BeNull();
        }

        [Fact]
        public async Task Authenticate_ReturnErrorResponse_IfUserNotExists()
        {
            // Arrange
            User mockUser = null;
            UserDto mockUserDto = null;
            var mockLoginModel = new LoginModel
            {
                Username = "Test",
                Password = "Test123$"
            };

            var mockJwtService = new Mock<IJwtService>();
            mockJwtService.Setup(x => x.GenerateJwtToken(mockUser)).Returns(new JwtSecurityToken());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<UserDto>(mockUser)).Returns(mockUserDto);

            var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
            var mockRoleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(x => x.FindByNameAsync(mockLoginModel.Username)).ReturnsAsync(mockUser);
            mockUserManager.Setup(x => x.CheckPasswordAsync(mockUser, mockLoginModel.Password)).ReturnsAsync(false);

            // Act
            var service = new AuthenticationService(
                mockRoleManager.Object,
                mockUserManager.Object,
                mockJwtService.Object,
                mockMapper.Object);
            var result = await service.AuthenticateAsync(mockLoginModel);

            // Assert
            result.Should().BeOfType<LoginResponse>();
            result.Result.Should().BeFalse();
            result.Status.Should().Be(ResponseStatus.Error);
            result.Item.Should().BeNull();
            result.Token.Should().BeNull();
        }

        //[Fact]
        //public async Task Register_ReturnSuccessResponse_ForValidRegisterModel()
        //{
        //    // Arrange
        //    User mockUser = null;
        //    var mockLoginModel = new LoginModel
        //    {
        //        Username = "Test",
        //        Password = "Test123$"
        //    };

        //    var mockConfig = new Mock<IConfiguration>();
        //    var mockRoleManager = new Mock<RoleManager<IdentityRole>>();
        //    var mockUserManager = new Mock<UserManager<User>>();
        //    mockUserManager.Setup(x => x.FindByNameAsync(mockLoginModel.Username)).ReturnsAsync(mockUser);
        //    mockUserManager.Setup(x => x.CheckPasswordAsync(mockUser, mockLoginModel.Password)).ReturnsAsync(true);

        //    // Act
        //    var service = new AuthenticationService(mockConfig.Object, mockRoleManager.Object,
        //        mockUserManager.Object);
        //    var result = await service.AuthenticateAsync(mockLoginModel);

        //    // Assert
        //    result.Should().BeOfType<LoginResponse>();
        //    result.Result.Should().BeTrue();
        //    result.Status.Should().Be(ResponseStatus.Success);
        //    result.Username.Should().Be(mockLoginModel.Username);
        //}

        //[Fact]
        //public async Task Register_ReturnErrorResponse_IfUserAlreadyExists()
        //{
        //    // Arrange


        //    // Act
        //    var service = new AuthenticationService();
        //    var result = await service.RegisterAsync();

        //    // Assert
        //    result.Should().BeOfType<RegisterResponse>();
        //    result.Result.Should().BeFalse();
        //    result.Status.Should().Be(ResponseStatus.Error);
        //    result.Item.Id.Should().Be(mockGuest.Id);
        //    result.Item.FirstName.Should().Be(mockGuest.FirstName);
        //    result.Item.LastName.Should().Be(mockGuest.LastName);
        //}

        //[Fact]
        //public async Task Register_ReturnErrorResponse_ForNullParam()
        //{
        //    // Arrange


        //    // Act
        //    var service = new GuestService(mockMapper.Object, mockRepo.Object);
        //    var result = await service.CreateGuestAsync(mockGuestDto);

        //    // Assert
        //    result.Should().BeOfType<RegisterResponse>();
        //    result.Result.Should().BeFalse();
        //    result.Status.Should().Be(ResponseStatus.Error);
        //    result.Item.Should().BeNull();
        //}

        //[Fact]
        //public async Task CreateGuestAsync_ReturnErrorResponse_IfExceptionThrown()
        //{
        //    // Arrange


        //    // Act
        //    var service = new AuthenticationService();
        //    var result = await service.RegisterAsync();

        //    // Assert
        //    result.Should().BeOfType<RegisterResponse>();
        //    result.Result.Should().BeFalse();
        //    result.Status.Should().Be(ResponseStatus.Error);
        //    result.Item.Should().BeNull();
        //    result.Message.Should().Contain("test message");
        //}
    }
}
