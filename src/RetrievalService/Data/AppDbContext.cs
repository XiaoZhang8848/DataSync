using Microsoft.EntityFrameworkCore;
using RetrievalService.Entities;

namespace RetrievalService.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
