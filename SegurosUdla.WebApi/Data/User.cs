using System;
using Microsoft.AspNetCore.Identity;

namespace SegurosUdla.WebApi.Data
{
    public class User : IdentityUser
    {
        public User()
        {
        }

        public User(string userName) : base(userName)
        {
        }
    }
}

