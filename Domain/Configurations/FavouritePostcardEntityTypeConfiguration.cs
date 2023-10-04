using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class FavouritePostcardEntityTypeConfiguration : IEntityTypeConfiguration<FavouritePostcard>
{
    public void Configure(EntityTypeBuilder<FavouritePostcard> builder)
    {
        builder.Property(p => p.Order).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.PostcardId).IsRequired();
    }
}