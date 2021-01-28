using Microsoft.AspNetCore.Identity;
using System;

namespace WeddingPlanner.Core.Domain
{
    public class User : IdentityUser
    {
        public User CreatedBy { get; }
        public User UpdatedBy { get; set; }

        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; set; }

        public User()
        { }

        public User(RegisterModel registerModel) : base()
        {
            Email = registerModel.Email;
            SecurityStamp = Guid.NewGuid().ToString();
            UserName = registerModel.Username;
        }
    }
}
