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

                  

                    //Save changes
                    if (isCanSaveChanges)
                    {
                        await context.SaveChangesAsync();
                    }

                    scope.Complete();
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
