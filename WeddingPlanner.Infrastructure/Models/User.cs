using Microsoft.AspNetCore.Identity;
using System;
using WeddingPlanner.Infrastructure.Models.Authentication;

namespace WeddingPlanner.Infrastructure.Models
{
    public class User : IdentityUser
    {
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
