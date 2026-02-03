using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class PersonsDbContext : DbContext
{
    public PersonsDbContext(DbContextOptions<PersonsDbContext> options) : base(options) { }
    public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("persons");
        modelBuilder.Entity<Person>(
            person =>
            {
                person.ToTable("Persons");
                person.HasKey(p => p.Id);
                person.Property(p => p.Id).ValueGeneratedOnAdd();
                person.Property(p => p.Name).IsRequired().HasMaxLength(250);
                person.Property(p => p.Gender).IsRequired(false);
                person.Property(p => p.Age).IsRequired(false);
                person.Property(p => p.Identification).HasMaxLength(25).IsUnicode(true);
                person.Property(p => p.Address).IsRequired().HasMaxLength(300);
                person.Property(p => p.PhoneNumber).HasMaxLength(25).IsRequired();
            }
        );

    }
}
