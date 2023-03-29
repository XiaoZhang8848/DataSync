using Microsoft.EntityFrameworkCore;
using ParseService.Entities;

namespace ParseService.Data;

public class AppDbContext: DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}