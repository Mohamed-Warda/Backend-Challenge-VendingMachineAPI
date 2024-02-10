using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Infrastructure.DbContext;

public class VendingMachineDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{

    public DbSet<Transaction> Transactions { get; set; }
    //  public DbSet<Deposit> Deposits { get; set; }
    public DbSet<Product> Products { get; set; }
    public VendingMachineDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Transaction>()
              .Property(d => d.IsConfirmed)
              .HasDefaultValue(false);


        builder.Entity<Transaction>()
                    .HasOne(t => t.User)
                    .WithMany()
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<User>()
            .Property(x => x.Deposit)
            .HasDefaultValue(0);
        new DbInitializer(builder).Seed();
    }
}
