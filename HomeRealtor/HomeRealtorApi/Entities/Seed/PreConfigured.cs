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

        public static IEnumerable<RealEstate> GetPreconfiguredRealEstates()
        {
            return new List<RealEstate>
            {
                new RealEstate()
                {
                    Image = "kapustinka",
                    StateName = "Budinochok",
                    Price = 255000,
                    Location = "Juravlina 8",
                    TimeOfPost = DateTime.Now,
                    TerritorySize = 250,
                    Active = true,
                    TypeId = 1,
                    UserId = "f92c2cdf-9914-422c-913d-4fc4d8a9e033"
                }
            };
        }

        public static IEnumerable<RealEstateType> GetPreconfiguredRealEstateTypes()
        {
            return new List<RealEstateType>
            {
                new RealEstateType()
                {
                    TypeName = "Budinok"
                }
            };
        }
    }
}
