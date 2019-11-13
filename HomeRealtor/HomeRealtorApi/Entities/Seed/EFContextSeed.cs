using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace HomeRealtorApi.Entities.Seed
{

    public class EFContextSeed
    {
        public static async Task SeedAsync(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var services = scope.ServiceProvider;
                using (var context = scope.ServiceProvider.GetService<EFContext>())
                {
                    var efContext = services.GetRequiredService<EFContext>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    await Seed(efContext, userManager, configuration);
                }
            }
        }
        private static async Task Seed(
            EFContext context,
            UserManager<User> usermanager,
            IConfiguration configuration,
            int? retry = 0
            )
            {
            int retryForAvailability = retry.Value;

            try
            {
                bool isCanSaveChanges = false;

                context.Database.Migrate();
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Seed role in db
                    if (!context.Roles.Any())
                    {
                        context.Roles.AddRange(
                            PreConfigured.GetPreconfiguredRoles()
                        );
                        isCanSaveChanges = true;
                    }


                    //suda pisat rofliki s rolyami
                    if(!context.RealEstateTypes.Any())
                    {
                        context.RealEstateTypes.AddRange(PreConfigured.GetPreconfiguredRealEstateTypes());
                        isCanSaveChanges = true;
                    }

                    if (!context.Users.Any())
                    {
                        User user = new User()
                        {
                            Email = "admin@hr.com",
                            FirstName = "Super",
                            LastName = "Admin",
                            UserName = "superAdmin",
                            CountOfLogins=0
                        };

                        await usermanager.CreateAsync(user, "Qwerty-1");
                        await usermanager.AddToRoleAsync(user, "Admin");
                    }
                    

                    if (!context.RealEstateSellTypes.Any())
                    {
                        context.RealEstateSellTypes.AddRange(PreConfigured.GetPreconfiguredRealEstateSellTypes());
                        isCanSaveChanges = true;
                    }
                    //if (!context.Regions.Any())
                    //{
                    //    context.Regions.AddRange(PreConfigured.GetPreconfiguredRegions());
                    //    isCanSaveChanges = true;
                    //}
                    //if (!context.DistrictTypes.Any())
                    //{
                    //    context.DistrictTypes.AddRange(PreConfigured.GetPreconfiguredDistrictTypes());
                    //    isCanSaveChanges = true; 
                        
                    //}
                    //if (!context.Towns.Any())
                    //{
                    //    context.Towns.AddRange(PreConfigured.GetPreconfiguredTowns());
                    //    isCanSaveChanges = true;
                    //}
                    
                    //if (!context.Districts.Any())
                    //{
                    //    context.Districts.AddRange(PreConfigured.GetPreconfiguredDistricts());
                    //    isCanSaveChanges = true;
                    //}
                    //suda pisat rofliki s rolyami
                    if (!context.RealEstates.Any())
                    {
                        //context.RealEstates.AddRange(PreConfigured.GetPreconfiguredRealEstates());
                        Faker<RealEstate> estatesFaked = new Faker<RealEstate>()
                            .RuleFor(t => t.Image, f => "1.jpg")
                            .RuleFor(t => t.Price, f => f.Random.Double(500_000, 700_000))
                            .RuleFor(
                                t => t.ImageEstates,
                                (f, t) => new List<ImageEstate>()
                                {
                                    new ImageEstate()
                                    {
                                        EstateId = t.Id,
                                        LargeImage = "2.jpg",
                                        MediumImage = "3.jpg",
                                        SmallImage = "4.jpg"
                                    },
                                    new ImageEstate()
                                    {
                                        EstateId = t.Id,
                                        LargeImage = "2.jpg",
                                        MediumImage = "3.jpg",
                                        SmallImage = "4.jpg"
                                    },
                                    new ImageEstate()
                                    {
                                        EstateId = t.Id,
                                        LargeImage = "2.jpg",
                                        MediumImage = "3.jpg",
                                        SmallImage = "4.jpg"
                                    }
                                })
                            .RuleFor(t => t.Location, f => f.Address.StreetName())
                            .RuleFor(t => t.StateName, f => f.Name.FullName())
                            .RuleFor(t => t.SellType, f => 1)
                            .RuleFor(t => t.TerritorySize, f => f.Random.Double(40, 500))
                            .RuleFor(t => t.TimeOfPost, f => DateTime.Now)
                            .RuleFor(t => t.UserId, f => context.Users.First().Id)
                            .RuleFor(t => t.Coordinates, f => "50.6183045,26.2388199")
                            .RuleFor(t => t.Description, f => f.Lorem.Text())
                            .RuleFor(t => t.TypeId, f => f.Random.Int(1, 3))
                            .RuleFor(t => t.RoomCount, f => f.Random.Int(1, 10))
                            .RuleFor(t => t.Active, f => true);
                        var estates = estatesFaked.Generate(1000);
                        context.RealEstates.AddRange(estates);
                        
                        isCanSaveChanges = true;
                    }

                    //if (!context.RealEstates.Any())
                    //{
                    //    context.RealEstates.AddRange(PreConfigured.GetPreconfiguredRealEstates());
                    //    isCanSaveChanges = true;
                    //}


                    //Save changes
                    if (isCanSaveChanges)
                    {
                        await context.SaveChangesAsync();
                    }

                    scope.Complete();




                    User user = new User()
                    {
                        UserName = "admin",
                        Email = "admin@gmail.com",
                        Age = 12,
                        PhoneNumber ="0503458675",
                        FirstName = "Jesus",
                        AboutMe = "I admin hello, I can BAN you",
                        LastName = "unknown",
                        CountOfLogins = 0
                    };
                    await usermanager.AddToRoleAsync(user, "Admin");
                    var result = await usermanager.CreateAsync(user, "Qwerty-1");
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    await Seed(context, usermanager, configuration, retryForAvailability);
                }
            }
        }
    }
}
