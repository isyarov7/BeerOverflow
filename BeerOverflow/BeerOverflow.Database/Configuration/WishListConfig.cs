using BeerOverflow.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Database.Configuration
{
    public class WishListConfig : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.HasOne(w => w.Beer)
                .WithMany(b => b.Wishlist)
                .HasForeignKey(w => w.BeerId);

            builder.HasOne(u => u.User)
                .WithMany(w => w.Wishlist)
                .HasForeignKey(u => u.UserId);
        }
    }
}
