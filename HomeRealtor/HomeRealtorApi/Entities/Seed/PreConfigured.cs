﻿using Microsoft.AspNetCore.Identity;
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
    }
}
