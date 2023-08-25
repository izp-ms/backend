using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class UserPostcardEntityTypeConfiguration : IEntityTypeConfiguration<UserPostcard>
{
    public void Configure(EntityTypeBuilder<UserPostcard> builder)
    {
        builder.HasKey(up => new { up.UserId, up.PostcardId });

        builder.HasOne(up => up.User)
            .WithMany(u => u.UserPostcards)
            .HasForeignKey(up => up.UserId);

        builder.HasOne(up => up.Postcard)
            .WithMany(p => p.UserPostcards)
            .HasForeignKey(up => up.PostcardId);
    }
}