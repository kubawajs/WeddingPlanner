using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Api.Controllers;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Models.Abstractions;
using WeddingPlanner.Infrastructure.Services.Abstractions;
using Xunit;

namespace WeddingPlanner.Tests.Controllers
{
    public class GuestsControllerUnitTest
    {
        [Fact]
        public async Task Index_ReturnsOkObjectResult_WithListOfGuests()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<GuestsController>>();
            var mockService = new Mock<IGuestService>();
            var mockGuests = new List<GuestDto>(new GuestDto[]
            {
                new GuestDto(),
                new GuestDto(),
                new GuestDto()
            });
            var mockItem = new GuestListDto
            {
                Guests = mockGuests
            };
            var mockResponse = new GuestListResponse(
                BaseApiResponse<GuestListDto>.CreateSuccessResponse("test message", mockItem))
            {
                Count = mockGuests.Count
            };
            mockService.Setup(repo => repo.GetGuestsAsync()).ReturnsAsync(mockResponse);
            var controller = new GuestsController(mockLogger.Object, mockService.Object);

            // Act
            var result = await controller.Index();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okObjectResult = (OkObjectResult) result;
            okObjectResult.Value.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<GuestListResponse>();

            var response = (GuestListResponse) okObjectResult.Value;
            response.Status.Should().Be(ResponseStatus.Success);
            response.Result.Should().BeTrue();
            response.Item.Guests.Should().NotBeNull();
            response.Item.Guests.Should().NotBeEmpty();
            response.Item.Guests.Should().HaveCount(3);
            response.Count.Should().Be(3);
        }

        [Fact]
        public async Task Index_ReturnsOkObjectResult_WithListOfGuests_ForGivenAgeParam()
        {
            // Arrange
            int ageParam = 3;
            var mockLogger = new Mock<ILogger<GuestsController>>();
            var mockService = new Mock<IGuestService>();
            var mockGuests = new List<GuestDto>(new GuestDto[]
            {
                new GuestDto(),
                new GuestDto(),
                new GuestDto()
            });
            var mockItem = new GuestListDto
            {
                Guests = mockGuests
            };
            var mockResponse = new GuestListResponse(
                BaseApiResponse<GuestListDto>.CreateSuccessResponse("test message", mockItem))
            {
                Count = mockGuests.Count
            };
            mockService.Setup(svc => svc.GetGuestsByAgeAsync(ageParam)).ReturnsAsync(mockResponse);
            var controller = new GuestsController(mockLogger.Object, mockService.Object);

            // Act
            var result = await controller.Index(ageParam);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okObjectResult = (OkObjectResult)result;
            okObjectResult.Value.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<GuestListResponse>();

            var response = (GuestListResponse)okObjectResult.Value;
            response.Status.Should().Be(ResponseStatus.Success);
            response.Result.Should().BeTrue();
            response.Item.Guests.Should().NotBeNull();
            response.Item.Guests.Should().NotBeEmpty();
            response.Item.Guests.Should().HaveCount(3);
            response.Count.Should().Be(3);
        }
        
        // To be performed when model attributes will be added
        //[Fact]
        //public async Task Create_ReturnsBadRequest_IfModelStateNotValid()
        //{
        //    // Arrange
        //    var mockLogger = new Mock<ILogger<GuestsController>>();
        //    var mockService = new Mock<IGuestService>();
        //    var controller = new GuestsController(mockLogger.Object, mockService.Object);

        //    // Act
        //    var result = await controller.Create(null);

        //    // Assert
        //    result.Should().BeOfType<BadRequestResult>();
        //}

        [Fact]
        public async Task Create_ReturnsOkResult_WithGuest_ForGivenGuestDto()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<GuestsController>>();
            var mockService = new Mock<IGuestService>();
            var mockGuest = new GuestDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };
            var mockItem = new GuestDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };
            var mockResponse = new GuestResponse(
                BaseApiResponse<GuestDto>.CreateSuccessResponse("test message", mockItem));
            mockService.Setup(svc => svc.CreateAsync(mockGuest)).ReturnsAsync(mockResponse);
            var controller = new GuestsController(mockLogger.Object, mockService.Object);

            // Act
            var result = await controller.Create(mockGuest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            
            var okObjectResult = (OkObjectResult) result;
            okObjectResult.Value.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<GuestResponse>();

            var response = (GuestResponse)okObjectResult.Value;
            response.Result.Should().BeTrue();
            response.Status.Should().Be(ResponseStatus.Success);
            response.Item.Id.Should().Be(mockGuest.Id);
            response.Item.FirstName.Should().Be(mockGuest.FirstName);
            response.Item.LastName.Should().Be(mockGuest.LastName);
        }
    }
}
