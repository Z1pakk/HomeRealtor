﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Models
{

    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Role { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string AboutMe { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
    }

}
