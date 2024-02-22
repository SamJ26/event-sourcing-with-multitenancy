using EventSourcing.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.Persistence;

public sealed class TenantDbContext(string connectionString) : DbContext
{
    private readonly string _connectionString = connectionString;

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_connectionString);
    }

    public DbSet<GameEntity> Games { get; init; } = null!;
}