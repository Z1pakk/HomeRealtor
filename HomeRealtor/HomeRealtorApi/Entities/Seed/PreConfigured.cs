using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities.Seed
{
    public class PreConfigured
    {
        public static IEnumerable<IdentityRole> GetPreconfiguredRoles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole("Admin")
                    {
                        NormalizedName ="ADMIN"
                    },
                new IdentityRole("Realtor")
                    {
                        NormalizedName ="REALTOR"
                    },
                new IdentityRole("User")
                    {
                        NormalizedName ="USER"
                    },
            };
        }

        public static IEnumerable<Advertising> GetPreconfiguredAdvertisings()
        {
            return new List<Advertising>
            {
                new Advertising()
                {
                    StateName = "Apartament",
                    Price = 2_000_000,
                    Contacts = "+380547896325",
                    Image = "wwwroot\\Content\\first.jfif",
                    UserId = "admin@admin.com"
                },

                new Advertising()
                {
                    StateName = "Building",
                    Price = 20_000_000,
                    Contacts = "+380987825325",
                    Image = "wwwroot\\Content\\secondimage.jfif",
                    UserId = "admin@admin.com"
                }
            };
        }

        public static IEnumerable<User> GetPreconfiguredUsers()
        {
            return new List<User>
            {
                new User()
                {
                    Email = "admin@admin.com",
                    UserName = "Admin",
                    PhoneNumber = "+380547896325",
                    FirstName = "Admin",
                    Age = 25
                }
            };
        }
    }
}
