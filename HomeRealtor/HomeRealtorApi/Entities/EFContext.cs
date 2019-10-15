using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    public class EFContext:DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserRole>().HasKey(t => new { t.RoleId, t.UserId });
        }

        public virtual DbSet<ImageEstate> ImageEstates { get; set; }
        public virtual DbSet<ImageUser> ImageUsers { get; set; }
        public virtual DbSet<RealEstate> RealEstates { get; set; }
        public virtual DbSet<RealEstateType> RealEstateTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<News> News{ get; set; }
    }
}
