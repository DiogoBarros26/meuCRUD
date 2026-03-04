using AgendaCrud_1.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaCrud_1.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }
}
