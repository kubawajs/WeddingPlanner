using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeddingPlanner.Infrastructure.Data;
using WeddingPlanner.Infrastructure.Dto;
using WeddingPlanner.Infrastructure.Mapping;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Repository;
using WeddingPlanner.Infrastructure.Services;
using Xunit;

namespace WeddingPlanner.Tests.Services
{
    public class GuestServiceIntegrationTest
    {
        private IEnumerable<Guest> _guests = new List<Guest>(new Guest[]
        {
            new Guest { FirstName = "Tom", LastName = "Holland", BirthDate = DateTime.Now.AddYears(-30) },
            new Guest { FirstName = "Sarah", LastName = "Connor", BirthDate = DateTime.Now.AddYears(-4) },
            new Guest { FirstName = "Robert", LastName = "Kubica", BirthDate = DateTime.Now.AddYears(-4).AddDays(1) }
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
        public async Task GetGuestsAsync_ReturnsGuestDtoList_IfExistsInRepo()
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
            result.Items.Should().NotBeNull();
            result.Items.Should().HaveCount(3);
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
            result.Items.Should().NotBeNull();
            result.Items.Should().BeEmpty();
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
            result.Items.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetGuestsByAgeAsync_ReturnsEmptyList_IfNoGuestsAboveParam()
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
            result.Items.Should().NotBeNull();
            result.Items.Should().BeEmpty();
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
