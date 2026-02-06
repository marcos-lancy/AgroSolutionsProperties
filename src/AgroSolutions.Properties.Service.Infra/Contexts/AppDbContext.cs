using AgroSolutions.Properties.Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgroSolutions.Properties.Service.Infra.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<PropriedadeEntity> Propriedades => Set<PropriedadeEntity>();
    public DbSet<TalhaoEntity> Talhoes => Set<TalhaoEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Entity<PropriedadeEntity>()
            .HasMany(p => p.Talhoes)
            .WithOne(t => t.Propriedade)
            .HasForeignKey(t => t.PropriedadeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.EnableSensitiveDataLogging();
    }
}
