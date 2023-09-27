using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class PostcardEntityTypeConfiguration : IEntityTypeConfiguration<Postcard>
{
    public void Configure(EntityTypeBuilder<Postcard> builder)
    {
        builder.HasOne(p => p.PostcardData)
            .WithMany(p => p.Postcards)
            .HasForeignKey(p => p.PostcardDataId);

        builder.Property(p => p.Title).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Content).HasMaxLength(280).IsRequired();
        builder.Property(p => p.Type).HasMaxLength(20).IsRequired();
    }
}