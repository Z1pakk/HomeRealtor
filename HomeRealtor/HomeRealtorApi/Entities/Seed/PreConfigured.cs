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
    }
}
