using FluentAssertions;
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
            var result = BaseApiResponse.CreateErrorResponse(testMessage);

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

            // Act
            var result = BaseApiResponse.CreateSuccessResponse(testMessage);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeTrue();
            result.Status.Should().Be(ResponseStatus.Success);
            result.Message.Should().Be(testMessage);
        }
    }
}
