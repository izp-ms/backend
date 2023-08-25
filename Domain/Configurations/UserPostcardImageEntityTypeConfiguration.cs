using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class PostcardImageEntityTypeConfiguration : IEntityTypeConfiguration<PostcardImage>
{
    public void Configure(EntityTypeBuilder<PostcardImage> builder)
    {
        builder.Property(p => p.ImageBase64).IsRequired();
    }
}