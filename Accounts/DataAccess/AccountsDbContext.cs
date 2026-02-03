using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class AccountsDbContext : DbContext
{
    public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base(options) { }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("account_db");
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Accounts");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).ValueGeneratedOnAdd();
            entity.Property(a => a.PersonId).IsRequired();

            entity.Property(a => a.AccountNumber)
                  .IsRequired()
                  .HasMaxLength(50);
            entity.HasIndex(a => a.AccountNumber).IsUnique();

            entity.Property(a => a.OwnerName)
                  .IsRequired()
                  .HasMaxLength(250);

            entity.Property(a => a.AccountType)
                  .HasMaxLength(50);

            entity.Property(a => a.Balance)
                  .IsRequired()
                  .HasPrecision(18, 2);

            entity.Property(a => a.Status).IsRequired();
            entity.Property(a => a.CreatedAt).HasColumnType("date").IsRequired();

        });
    }
}
