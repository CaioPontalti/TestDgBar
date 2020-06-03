using Clearsale.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clearsale.Domain.Models
{
    public class User : Entity
    {
        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public User()
        {

        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
