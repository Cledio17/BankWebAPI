﻿using Microsoft.Extensions.FileProviders;

namespace BankWebAPI.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string pfp { get; set; }
    }
}
