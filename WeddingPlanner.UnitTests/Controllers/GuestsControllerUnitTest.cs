using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Api.Controllers;
using WeddingPlanner.Infrastructure.Dto;
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
            mockService.Setup(repo => repo.GetGuestsAsync()).ReturnsAsync(mockGuests);
            var controller = new GuestsController(mockLogger.Object, mockService.Object);

            // Act
            var result = await controller.Index();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okObjectResult = (OkObjectResult) result;
            okObjectResult.Value.Should().NotBeNull();
            okObjectResult.Value.Should().BeAssignableTo<IEnumerable<GuestDto>>();

            var guestDtosObject = (IEnumerable<GuestDto>) okObjectResult.Value;
            guestDtosObject.Should().NotBeNull();
            guestDtosObject.Should().NotBeEmpty();
            guestDtosObject.Should().HaveCount(3);
        }

        [Fact]
        public async Task Index_ReturnsBadRequest_IfExceptionThrown()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<GuestsController>>();
            var mockService = new Mock<IGuestService>();
            mockService.Setup(repo => repo.GetGuestsAsync()).Throws(new System.Exception());
            var controller = new GuestsController(mockLogger.Object, mockService.Object);

            // Act
            var result = await controller.Index();

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_IfModelStateNotValid()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<GuestsController>>();
            var mockService = new Mock<IGuestService>();
            var controller = new GuestsController(mockLogger.Object, mockService.Object);

            // Act
            var result = await controller.Create(null);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WithGuest_ForGivenGuestDto()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<GuestsController>>();
            var mockService = new Mock<IGuestService>();
            var controller = new GuestsController(mockLogger.Object, mockService.Object);
            var testGuest = new GuestDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };

            // Act
            var result = await controller.Create(testGuest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            
            var okObjectResult = (OkObjectResult) result;
            okObjectResult.Value.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<GuestDto>();

            var guestDtoObject = (GuestDto)okObjectResult.Value;
            guestDtoObject.Id = testGuest.Id;
            guestDtoObject.FirstName = testGuest.FirstName;
            guestDtoObject.LastName = testGuest.LastName;
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_IfExceptionThrown()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<GuestsController>>();
            var mockService = new Mock<IGuestService>();
            mockService.Setup(repo => repo.CreateGuestAsync(new GuestDto())).Throws(new System.Exception());
            var controller = new GuestsController(mockLogger.Object, mockService.Object);

            // Act
            var result = await controller.Create(null);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}
