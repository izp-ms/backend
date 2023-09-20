using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(p => p.UsersStats)
            .WithOne(p => p.User)
            .HasForeignKey<UserStat>(p => p.Id);
        builder.HasOne(p => p.Address)
            .WithOne(p => p.User)
            .HasForeignKey<Address>(p => p.Id);
        builder.HasOne(p => p.UsersDetails)
            .WithOne(p => p.User)
            .HasForeignKey<UserDetail>(p => p.Id);

        builder.HasMany(p => p.Postcards)
            .WithMany(p => p.Users)
            .UsingEntity<UserPostcard>(
                p => p.HasOne(u => u.Postcard)
                .WithMany()
                .HasForeignKey(u => u.PostcardId),

                p => p.HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId),

                p =>
                {
                    p.HasKey(x => x.Id);
                    p.Property(x => x.CreatedAt).HasDefaultValueSql("getutcdate()");
                }
            );

        builder.Property(p => p.Email).IsRequired().HasMaxLength(320);
        builder.Property(p => p.Password).IsRequired().HasMaxLength(255);
        builder.Property(p => p.NickName).IsRequired().HasMaxLength(32);
        builder.Property(p => p.Role).IsRequired().HasMaxLength(16);
        builder.Property(p => p.CreatedAt).IsRequired();
    }
}