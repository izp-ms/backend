using Domain.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserStat> UsersStats { get; set; }
    public DbSet<UserDetail> UsersDetails { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<Postcard> Postcards { get; set; }
    public DbSet<PostcardImage> PostcardsImages { get; set; }
    public DbSet<UserPostcard> UserPostcards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserStatsEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AddressEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserDetailsEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PostcardImageEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PostcardEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserPostcardEntityTypeConfiguration());
    }
}
