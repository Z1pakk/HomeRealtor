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
                    RoomCount = 8,
                    TerritorySize = 250,
                    Active = true,
                    TypeId = 1,
                    UserId = "ae6767bc-088b-4ab6-8f7c-1aacff200e6d",
                    SellType = 1
                },
                new RealEstate()
                {
                    Image = "pomidorchik",
                    StateName = "Kvartirka",
                    Price = 25000,
                    Location = "Bulby Borovcia bud.1, kv.3",
                    TimeOfPost = DateTime.Now,
                    RoomCount = 1,
                    TerritorySize = 60,
                    Active = true,
                    TypeId = 2,
                    UserId = "ae6767bc-088b-4ab6-8f7c-1aacff200e6d",
                    SellType = 1
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
                },
                new RealEstateType()
                {
                    TypeName = "Appartment"
                },
                new RealEstateType()
                {
                    TypeName = "Territory"
                }
            };
        }
        public static IEnumerable<Region> GetPreconfiguredRegions()
        {
            return new List<Region>
            {
                new Region()
                {
                    NameOfRegion="Rivnenska"
                },
                new Region()
                {
                    NameOfRegion="Volunska"
                },
                new Region()
                {
                    NameOfRegion="Kyivska"
                },
                new Region()
                {
                    NameOfRegion="Lvivska"
                },
                new Region()
                {
                    NameOfRegion="Kharkivska"
                },
                new Region()
                {
                    NameOfRegion="Poltavska"
                }
            };
        }
        public static IEnumerable<Town> GetPreconfiguredTowns()
        {
            return new List<Town>
            {
                new Town()
                {
                    RegionId=1,
                    NameOfTown="Rivne"
                },
                new Town()
                {
                    RegionId=2,
                    NameOfTown="Lutsk"
                },
                new Town()
                {
                    RegionId=3,
                    NameOfTown="Kyiv"
                },
                new Town()
                {
                    RegionId=4,
                    NameOfTown="Lviv"
                },
                new Town()
                {
                    RegionId=5,
                    NameOfTown="Kharkiv"
                },
                new Town()
                {
                    RegionId=6,
                    NameOfTown="Poltava"
                }
            };
        }
        public static IEnumerable<DistrictType> GetPreconfiguredDistrictTypes()
        {
            return new List<DistrictType>
            {
                new DistrictType()
                {
                    NameOfType="Suburb"
                },
                new DistrictType()
                {
                    NameOfType="Village"
                },
                new DistrictType()
                {
                    NameOfType="District"
                }
            };
        }
        public static IEnumerable<District> GetPreconfiguredDistricts()
        {
            return new List<District>
            {
                new District()
                {
                    DistrictTypeId=1,
                    NameOfDistrict="Barmaki",
                    TownId=1
                },
                new District()
                {
                    DistrictTypeId=1,
                    NameOfDistrict="Zhutun",
                    TownId=1
                },
                new District()
                {
                    DistrictTypeId=2,
                    NameOfDistrict="Goroduchshe",
                    TownId=1
                },
                new District()
                {
                    DistrictTypeId=2,
                    NameOfDistrict="Gorodok",
                    TownId=1
                },
                new District()
                {
                    DistrictTypeId=3,
                    NameOfDistrict="Jubileyniy",
                    TownId=1
                },
                new District()
                {
                    DistrictTypeId=3,
                    NameOfDistrict="Boyarka",
                    TownId=1
                },
                new District()
                {
                    DistrictTypeId=1,
                    NameOfDistrict="Kivertsi",
                    TownId=2
                },
                new District()
                {
                    DistrictTypeId=3,
                    NameOfDistrict="Centre",
                    TownId=2
                },
                new District()
                {
                    DistrictTypeId=1,
                    NameOfDistrict="Troeschina",
                    TownId=3
                },
                new District()
                {
                    DistrictTypeId=3,
                    NameOfDistrict="Chapayevka",
                    TownId=3
                },
                new District()
                {
                    DistrictTypeId=1,
                    NameOfDistrict="Runok squaire",
                    TownId=4
                },
                new District()
                {
                    DistrictTypeId=3,
                    NameOfDistrict="Sokilnyki",
                    TownId=4
                },
                new District()
                {
                    DistrictTypeId=1,
                    NameOfDistrict="Saltivka",
                    TownId=5
                },
                new District()
                {
                    DistrictTypeId=3,
                    NameOfDistrict="Frunze",
                    TownId=5
                },
                new District()
                {
                    DistrictTypeId=1,
                    NameOfDistrict="Polovki",
                    TownId=6
                },
                new District()
                {
                    DistrictTypeId=3,
                    NameOfDistrict="Rybci",
                    TownId=6
                }
            };
        }
        public static IEnumerable<RealEstateSellType> GetPreconfiguredRealEstateSellTypes()
        {
            return new List<RealEstateSellType>
            {
                new RealEstateSellType()
                {
                    SellTypeName = "Sell"
                },
                new RealEstateSellType()
                {
                    SellTypeName = "Rent"
                },
                new RealEstateSellType()
                {
                    SellTypeName = "Sublease"
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
                    LastName = "Adminovich",
                    Age = 25
                }
            };
        }

        

       
    }
}
