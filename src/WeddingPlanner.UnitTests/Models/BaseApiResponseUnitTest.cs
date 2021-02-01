using FluentAssertions;
using Moq;
using WeddingPlanner.Infrastructure.Dto.Abstractions;
using WeddingPlanner.Infrastructure.Models.Abstractions;
using Xunit;

namespace WeddingPlanner.Tests.Models
{
    public class BaseApiResponseUnitTest
    {
        [Fact]
        public void CreateErrorResponse_ReturnsResponse_WithFalseResultAndErrorStatus()
        {
            // Arrange
            var testMessage = "Test message";

            // Act
            var result = BaseApiResponse<IDto>.CreateErrorResponse(testMessage);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeFalse();
            result.Status.Should().Be(ResponseStatus.Error);
            result.Message.Should().Be(testMessage);
        }

        [Fact]
        public void CreateSuccessResponse_ReturnsResponse_WithTrueResultAndSuccessStatus()
        {
            // Arrange
            var testMessage = "Test message";
            var mockDto = new Mock<IDto>();

            // Act
            var result = BaseApiResponse<IDto>.CreateSuccessResponse(testMessage, mockDto.Object);

            // Assert
            result.Should().NotBeNull();
            result.Item.Should().NotBeNull();
            result.Result.Should().BeTrue();
            result.Status.Should().Be(ResponseStatus.Success);
            result.Message.Should().Be(testMessage);
        }
    }
}
