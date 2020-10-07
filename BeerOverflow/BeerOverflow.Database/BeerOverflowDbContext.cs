using BeerOverflow.Database.Configuration;
using BeerOverflow.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
