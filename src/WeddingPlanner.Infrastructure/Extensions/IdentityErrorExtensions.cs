using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace WeddingPlanner.Infrastructure.Extensions
{
    public static class IdentityErrorExtensions
    {
        public static string GetErrorsAsString(this IEnumerable<IdentityError> errors, char separator = ' ') 
            => string.Join(separator, errors.Select(x => x.Description));
    }
}
