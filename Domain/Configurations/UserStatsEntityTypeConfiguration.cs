using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class UserStatsEntityTypeConfiguration : IEntityTypeConfiguration<UserStat>
{
    public void Configure(EntityTypeBuilder<UserStat> builder)
    {
        builder.Property(p => p.PostcardsSent).IsRequired();
        builder.Property(p => p.PostcardsReceived).IsRequired();
    }
}