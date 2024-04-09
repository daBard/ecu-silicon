using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)
{
    public virtual DbSet<AddressEntity> Addresses { get; set; }
 
    //public virtual DbSet<UserEntity> Users { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<UserEntity>()
    //        .HasIndex(x => x.Email)
    //        .IsUnique();
    //}
}
