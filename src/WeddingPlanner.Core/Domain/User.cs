using Microsoft.AspNetCore.Identity;
using System;

namespace WeddingPlanner.Core.Domain
{
    public class User : IdentityUser
    {
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; set; }

        public User()
        {
            var now = DateTime.Now;
            CreatedAt = now;
            UpdatedAt = now;
        }

        public User(RegisterModel registerModel) : base()
        {
            var now = DateTime.Now;
            CreatedAt = now;
            UpdatedAt = now;
            Email = registerModel.Email;
            SecurityStamp = Guid.NewGuid().ToString();
            UserName = registerModel.Username;
        }
    }
}
