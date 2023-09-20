﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class PostcardDataEntityTypeConfiguration : IEntityTypeConfiguration<PostcardData>
{
    public void Configure(EntityTypeBuilder<PostcardData> builder)
    {
        builder.Property(p => p.ImageBase64).IsRequired();
        builder.Property(p => p.Country).HasMaxLength(100).IsRequired();
        builder.Property(p => p.City).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Latitude).HasMaxLength(12).IsRequired();
        builder.Property(p => p.Longitude).HasMaxLength(12).IsRequired();
    }
}