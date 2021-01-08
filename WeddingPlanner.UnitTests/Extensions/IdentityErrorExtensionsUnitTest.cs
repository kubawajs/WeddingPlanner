using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WeddingPlanner.Infrastructure.Extensions;
using Xunit;

namespace WeddingPlanner.Tests.Extensions
{
    public class IdentityErrorExtensionsUnitTest
    {
        [Fact]
        public void GetErrorAsStringShouldReturnConcatenatedErrorListStringForListOfErrors()
        {
            // Arrange
            IEnumerable<IdentityError> errorList = new List<IdentityError>(
                new IdentityError[]
                {
                    new IdentityError { Description = "test" },
                    new IdentityError { Description = "error" },
                    new IdentityError { Description = "list" }
                });

            // Act
            var expected = "test error list";
            var result = errorList.GetErrorsAsString();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetErrorAsStringShouldReturnEmptyStringForEmptyErrorList()
        {
            // Arrange
            IEnumerable<IdentityError> errorList = new List<IdentityError>();

            // Act
            var result = errorList.GetErrorsAsString();

            // Assert
            Assert.Equal(string.Empty, result);
        }
    }
}
