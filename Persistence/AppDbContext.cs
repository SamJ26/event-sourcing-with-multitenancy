using Microsoft.EntityFrameworkCore;

namespace EventSourcing.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<InstanceEntity> Instances { get; init; } = null!;
}