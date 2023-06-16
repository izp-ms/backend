using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.Email).IsRequired().HasMaxLength(320);
        builder.Property(p => p.Password).IsRequired().HasMaxLength(255);
        builder.Property(p => p.NickName).IsRequired().HasMaxLength(32);
        builder.Property(p => p.Role).IsRequired().HasMaxLength(16);
        builder.Property(p => p.CreatedAt).IsRequired();
    }
}