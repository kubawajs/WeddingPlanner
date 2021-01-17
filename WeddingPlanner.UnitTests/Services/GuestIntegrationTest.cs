using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain;
using WeddingPlanner.Infrastructure.Data;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Mapping;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Repository;
using WeddingPlanner.Infrastructure.Services;
using Xunit;

namespace WeddingPlanner.Tests.Services
{
    public class GuestIntegrationTest
    {
        private IEnumerable<Guest> _guests = new List<Guest>(new Guest[]
        {
            new Guest { FirstName = "Tom", LastName = "Holland", Age = 30 },
            new Guest { FirstName = "Sarah", LastName = "Connor", Age = 3, IsChild = true },
            new Guest { FirstName = "Robert", LastName = "Kubica", Age = 4, IsChild = true }
        });

        [Fact]
        public async Task CreateGuestAsync_CreatesGuest_IfGuestDtoCorrect()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = mapperConfig.CreateMapper();
            var options = await SetupTestDatabaseAsync(_guests);
            var guestRepository = new GuestRepository(new WeddingPlannerDbContext(options));
            var guestService = new GuestService(mapper, guestRepository);

            // Act
            var guestDto = new GuestDto
            {
                FirstName = "John",
                LastName = "Doe"
            };
            await guestService.CreateGuestAsync(guestDto);

            // Assert
            using var context = new WeddingPlannerDbContext(options);
            var guests = await context.Guests.ToListAsync();
            var guest = await context.Guests.SingleAsync(x => x.Id == 4);

            guest.Should().NotBeNull();
            guest.Id.Should().Be(4);
            guest.FirstName.Should().Be("John");
            guest.LastName.Should().Be("Doe");
        }

        [Fact]
        public async Task GetGuestsAsync_ReturnsGuestListResponse_IfExistsInRepo()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = mapperConfig.CreateMapper();
            var options = await SetupTestDatabaseAsync(_guests);
            var guestRepository = new GuestRepository(new WeddingPlannerDbContext(options));
            var guestService = new GuestService(mapper, guestRepository);

            // Act
            var result = await guestService.GetGuestsAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GuestListResponse>();
            result.Item.Guests.Should().NotBeNull();
            result.Item.Guests.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetGuestsAsync_ReturnsEmptyList_IfNoGuests()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = mapperConfig.CreateMapper();
            var options = await SetupTestDatabaseAsync(new List<Guest>());
            var guestRepository = new GuestRepository(new WeddingPlannerDbContext(options));
            var guestService = new GuestService(mapper, guestRepository);

            // Act
            var result = await guestService.GetGuestsAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GuestListResponse>();
            result.Item.Guests.Should().NotBeNull();
            result.Item.Guests.Should().BeEmpty();
        }

        [Fact]
        public async Task GetGuestsByAgeAsync_ReturnsGuests_WithAgeGreaterThanParam()
        {
            // Arrange
            int ageParam = 4;
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = mapperConfig.CreateMapper();
            var options = await SetupTestDatabaseAsync(_guests);
            var guestRepository = new GuestRepository(new WeddingPlannerDbContext(options));
            var guestService = new GuestService(mapper, guestRepository);

            // Act
            var result = await guestService.GetGuestsByAgeAsync(ageParam);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GuestListResponse>();
            result.Item.Guests.Should().NotBeNull();
            result.Item.Guests.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetGuestsByAgeAsync_ReturnsAdultPeople_IfNoGuestsAboveParam()
        {
            // Arrange
            int ageParam = 99;
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = mapperConfig.CreateMapper();
            var options = await SetupTestDatabaseAsync(_guests);
            var guestRepository = new GuestRepository(new WeddingPlannerDbContext(options));
            var guestService = new GuestService(mapper, guestRepository);

            // Act
            var result = await guestService.GetGuestsByAgeAsync(ageParam);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GuestListResponse>();
            result.Item.Guests.Should().NotBeNull();
            result.Item.Guests.Should().HaveCount(1);
            result.Item.Guests.First().FirstName.Should().Be("Tom");
            result.Item.Guests.First().LastName.Should().Be("Holland");
            result.Item.Guests.First().Age.Should().Be(30);

        }

        private async Task<DbContextOptions<WeddingPlannerDbContext>> SetupTestDatabaseAsync(IEnumerable<Guest> items)
        {
            // Setup Test Database
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<WeddingPlannerDbContext>()
                .UseInMemoryDatabase(databaseName: dbName).Options;

            // Seed
            using (var context = new WeddingPlannerDbContext(options))
            {
                foreach(var item in items)
                {
                    await context.Guests.AddAsync(item);
                }
                await context.SaveChangesAsync();
            }

            return options;
        }
    }
}
