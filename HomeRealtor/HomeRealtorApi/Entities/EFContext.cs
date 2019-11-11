using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    public class EFContext: IdentityDbContext<User>
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<HomePlace>().HasKey(table => new {
                table.DistrictId,
                table.RealEstateId
            });
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public virtual DbSet<ImageEstate> ImageEstates { get; set; }
        public virtual DbSet<ImageUser> ImageUsers { get; set; }
        public virtual DbSet<RealEstate> RealEstates { get; set; }
        public virtual DbSet<RealEstateType> RealEstateTypes { get; set; }
        public virtual DbSet<News> News{ get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<UserUnlockCodes> UserUnlockCodes  { get; set; }
        public virtual DbSet<Advertising> Advertisings { get; set; }
        public virtual DbSet<RealEstateSellType> RealEstateSellTypes { get; set; }
        public virtual DbSet<ForgotPassword> ForgotPasswords { get; set; }
        public virtual DbSet<HomePlace> HomePlaces { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<DistrictType> DistrictTypes { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
    }
}
