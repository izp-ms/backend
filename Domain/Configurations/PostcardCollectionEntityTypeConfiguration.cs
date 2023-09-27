using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class PostcardCollectionEntityTypeConfiguration : IEntityTypeConfiguration<PostcardCollection>
{
    public void Configure(EntityTypeBuilder<PostcardCollection> builder)
    {
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.PostcardDataId).IsRequired();
    }
}