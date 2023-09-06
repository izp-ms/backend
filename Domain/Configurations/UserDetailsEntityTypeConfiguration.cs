using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class UserDetailsEntityTypeConfiguration : IEntityTypeConfiguration<UserDetail>
{
    public void Configure(EntityTypeBuilder<UserDetail> builder)
    {
        builder.Property(p => p.FirstName).HasMaxLength(35);
        builder.Property(p => p.LastName).HasMaxLength(35);
        builder.Property(p => p.Description).HasMaxLength(255);
        builder.Property(p => p.BirthDate).IsRequired(false);
    }
}