using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ClientsDbContext : IdentityDbContext<Client>
{
    public ClientsDbContext(DbContextOptions<ClientsDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
    public DbSet<Client> Clients { get; set; }
}
