using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ClientsDbContext : IdentityDbContext<Client>
{
    public ClientsDbContext(DbContextOptions<ClientsDbContext> options) : base(options) { }
    public DbSet<Client> Clients { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("clients");
        base.OnModelCreating(modelBuilder);
    }
}
