using Domain.Models.Movements;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class MovementsDbContext : DbContext
{
    public MovementsDbContext(DbContextOptions<MovementsDbContext> options) : base(options) { }
    public DbSet<Movement> Movements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("movements");
        modelBuilder.Entity<Movement>(entity =>
        {
            entity.ToTable("Movements");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.Property(a => a.AccountNumber)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(a => a.AccountType)
                  .HasMaxLength(50);

            entity.HasIndex(a => a.AccountNumber);

            entity.Property(a => a.Amount)
                  .IsRequired()
                  .HasPrecision(18, 2);

            entity.Property(a => a.Balance)
                  .IsRequired()
                  .HasPrecision(18, 2);

            entity.Property(a => a.Date)
                  .HasColumnType("date")
                  .IsRequired();
        });
    }

}
