using BeerOverflow.Database.Configuration;
using BeerOverflow.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeerOverflow.Database
{
    public class BeerOverflowDbContext : IdentityDbContext<User, Role, int>
    {
        public BeerOverflowDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BeerConfig());
            modelBuilder.ApplyConfiguration(new BreweryConfig());
            modelBuilder.ApplyConfiguration(new ReviewConfig());
            modelBuilder.ApplyConfiguration(new WishListConfig());

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "member",
                    NormalizedName = "MEMBER",
                },
                new Role
                {
                    Id = 2,
                    Name = "admin",
                    NormalizedName = "ADMIN",
                });

            var hasher = new PasswordHasher<User>();

            User admin = new User
            {
                Id = 1,
                UserName = "admin@admin.admin",
                NormalizedUserName = "ADMIN@ADMIN.ADMIN",
                Email = "admin@admin.admin",
                NormalizedEmail = "ADMIN@ADMIN.ADMIN",
                SecurityStamp = "7I5VHIJTSZNOT3KDWKNFUV5PVYBHGXN",
            };

            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

            modelBuilder.Entity<User>().HasData(admin);

            base.OnModelCreating(modelBuilder);
        }
    }
}
