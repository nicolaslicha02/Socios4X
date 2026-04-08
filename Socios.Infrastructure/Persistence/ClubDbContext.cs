using Microsoft.EntityFrameworkCore;
using Socios.Domain.Entities;

namespace Socios.Infrastructure.Persistence;

public class ClubDbContext : DbContext
{
    public ClubDbContext(DbContextOptions<ClubDbContext> options) : base(options) { }

    public DbSet<ApplicationSetting> ApplicationSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationSetting>(entity =>
        {
            entity.ToTable("Applications");
            entity.HasKey(e => e.Id);
        });
    }
}