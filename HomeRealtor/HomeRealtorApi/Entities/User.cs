﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tbl_Users")]
    public class User:IdentityUser
    {
        [Required, StringLength(20)]
        public string FirstName { get; set; }
        [Required, StringLength(20)]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        public int CountOfLogins { get; set; }
        public string AboutMe { get; set; }
        public string Image { get; set; }
        public DateTime RegisterDate { get; set; }
        public virtual ICollection<ImageUser> ImageUsers { get; set; }

        public virtual ICollection<UserUnlockCodes> UserUnlockCodes { get; set; }
        public virtual ICollection<Advertising> Advertisings { get; set; }
        public virtual ICollection<RealEstate> RealEstates { get; set; }
        public virtual ForgotPassword PasswordOff { get; set; }
    }
}
