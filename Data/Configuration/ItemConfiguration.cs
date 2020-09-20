using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Configuration
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasOne(p => p.Product)
                .WithMany(p => p.Items)
                .HasForeignKey(p => p.ProductId);

            builder.HasOne(p=>p.Status)
                .WithMany(p => p.Items)
                .HasForeignKey(p => p.StatusId);
        }
    }
}
