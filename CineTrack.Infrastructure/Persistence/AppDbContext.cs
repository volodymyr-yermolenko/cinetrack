using CineTrack.Domain.Entities;
using CineTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CineTrack.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Genre> Genres { get; set; } = default!;
    public DbSet<Movie> Movies { get; set; } = default!;

    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);    
    }
}