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
            base.OnModelCreating(builder);
        }

        public virtual DbSet<ImageEstate> ImageEstates { get; set; }
        public virtual DbSet<ImageUser> ImageUsers { get; set; }
        public virtual DbSet<RealEstate> RealEstates { get; set; }
        public virtual DbSet<RealEstateType> RealEstateTypes { get; set; }
        public virtual DbSet<News> News{ get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Advertising> Advertisings { get; set; }
    }
}
