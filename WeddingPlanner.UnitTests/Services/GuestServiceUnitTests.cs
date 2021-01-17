using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Models.Abstractions;
using WeddingPlanner.Infrastructure.Repository.Abstractions;
using WeddingPlanner.Infrastructure.Services;
using Xunit;

namespace WeddingPlanner.Tests.Services
{
    public class GuestServiceUnitTests
    {
        [Fact]
        public async Task CreateGuestAsync_ReturnSuccessResponse_ForValidGuestDto()
        {
            // Arrange
            var mockGuestDto = new GuestDto
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 30
            };
            var mockGuest = new Guest
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 30
            };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Guest>(mockGuestDto)).Returns(mockGuest);
            var mockRepo = new Mock<IGuestRepository>();

            // Act
            var service = new GuestService(mockMapper.Object, mockRepo.Object);
            var result = await service.CreateGuestAsync(mockGuestDto);

            // Assert
            result.Should().BeOfType<GuestResponse>();
            result.Result.Should().BeTrue();
            result.Status.Should().Be(ResponseStatus.Success);
            result.Item.Id.Should().Be(mockGuest.Id);
            result.Item.FirstName.Should().Be(mockGuest.FirstName);
            result.Item.LastName.Should().Be(mockGuest.LastName);
        }

        [Fact]
        public async Task CreateGuestAsync_ReturnErrorResponse_ForNullParam()
        {
            // Arrange
            GuestDto mockGuestDto = null;
            Guest mockGuest = null;

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Guest>(mockGuestDto)).Returns(mockGuest);
            var mockRepo = new Mock<IGuestRepository>();

            // Act
            var service = new GuestService(mockMapper.Object, mockRepo.Object);
            var result = await service.CreateGuestAsync(mockGuestDto);

            // Assert
            result.Should().BeOfType<GuestResponse>();
            result.Result.Should().BeFalse();
            result.Status.Should().Be(ResponseStatus.Error);
            result.Item.Should().BeNull();
        }

        [Fact]
        public async Task CreateGuestAsync_ReturnErrorResponse_IfExceptionThrown()
        {
            // Arrange
            var mockGuestDto = new GuestDto
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 30
            };
            var mockGuest = new Guest
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 30
            };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Guest>(mockGuestDto)).Returns(mockGuest);
            var mockRepo = new Mock<IGuestRepository>();
            mockRepo.Setup(x => x.CreateGuestAsync(mockGuest)).Throws(new Exception("test message"));

            // Act
            var service = new GuestService(mockMapper.Object, mockRepo.Object);
            var result = await service.CreateGuestAsync(mockGuestDto);

            // Assert
            result.Should().BeOfType<GuestResponse>();
            result.Result.Should().BeFalse();
            result.Status.Should().Be(ResponseStatus.Error);
            result.Item.Should().BeNull();
            result.Message.Should().Contain("test message");
        }

        [Fact]
        public async Task GetGuestsAsync_ReturnSuccessResponse_ForValidGuestDto()
        {
            // Arrange
            var mockGuests = new List<Guest>(new Guest[]
            {
                new Guest { FirstName = "John", LastName = "Doe", Age = 30 },
                new Guest { FirstName = "Jack", LastName = "Sparrow", Age = 23 },
                new Guest { FirstName = "John", LastName = "Rambo", Age = 33 }
            });
            var mockGuestsDto = new List<GuestDto>(new GuestDto[]
            {
                new GuestDto { FirstName = "John", LastName = "Doe", Age = 30 },
                new GuestDto { FirstName = "Jack", LastName = "Sparrow", Age = 23 },
                new GuestDto { FirstName = "John", LastName = "Rambo", Age = 33 }
            });

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<IEnumerable<GuestDto>>(mockGuests))
                      .Returns(mockGuestsDto);
            
            var mockRepo = new Mock<IGuestRepository>();
            mockRepo.Setup(x => x.GetGuestsAsync()).ReturnsAsync(mockGuests);

            // Act
            var service = new GuestService(mockMapper.Object, mockRepo.Object);
            var result = await service.GetGuestsAsync();

            // Assert
            result.Should().BeOfType<GuestListResponse>();
            result.Result.Should().BeTrue();
            result.Status.Should().Be(ResponseStatus.Success);
            result.Item.Guests.Should().NotBeNull();
            result.Item.Guests.Should().HaveCount(3);
            result.Count.Should().Be(3);
        }

        [Fact]
        public async Task GetGuestsAsync_ReturnErrorResponse_IfGuestsNull()
        {
            // Arrange
            IEnumerable<Guest> mockGuests = null;
            IEnumerable<GuestDto> mockGuestsDto = null;

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<IEnumerable<GuestDto>>(mockGuests))
                      .Returns(mockGuestsDto);

            var mockRepo = new Mock<IGuestRepository>();
            mockRepo.Setup(x => x.GetGuestsAsync()).ReturnsAsync(mockGuests);

            // Act
            var service = new GuestService(mockMapper.Object, mockRepo.Object);
            var result = await service.GetGuestsAsync();

            // Assert
            result.Should().BeOfType<GuestListResponse>();
            result.Result.Should().BeFalse();
            result.Status.Should().Be(ResponseStatus.Error);
            result.Count.Should().Be(0);
            result.Item.Should().BeNull();
        }

        [Fact]
        public async Task GetGuestsAsync_ReturnErrorResponse_IfExceptionThrown()
        {
            // Arrange
            var mockGuests = new List<Guest>(new Guest[]
            {
                new Guest { FirstName = "John", LastName = "Doe", Age = 30 },
                new Guest { FirstName = "Jack", LastName = "Sparrow", Age = 23 },
                new Guest { FirstName = "John", LastName = "Rambo", Age = 33 }
            });
            var mockGuestsDto = new List<GuestDto>(new GuestDto[]
            {
                new GuestDto { FirstName = "John", LastName = "Doe", Age = 30 },
                new GuestDto { FirstName = "Jack", LastName = "Sparrow", Age = 23 },
                new GuestDto { FirstName = "John", LastName = "Rambo", Age = 33 }
            });

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<IEnumerable<GuestDto>>(mockGuests))
                      .Returns(mockGuestsDto);

            var mockRepo = new Mock<IGuestRepository>();
            mockRepo.Setup(x => x.GetGuestsAsync()).Throws(new Exception("test exception"));

            // Act
            var service = new GuestService(mockMapper.Object, mockRepo.Object);
            var result = await service.GetGuestsAsync();

            // Assert
            result.Should().BeOfType<GuestListResponse>();
            result.Result.Should().BeFalse();
            result.Status.Should().Be(ResponseStatus.Error);
            result.Count.Should().Be(0);
            result.Item.Should().BeNull();
            result.Message.Should().Contain("test exception");
        }

        // TODO: GetGuestsByAgeAsync
    }
}
