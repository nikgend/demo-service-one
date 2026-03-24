using Microsoft.EntityFrameworkCore;
using TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;
using TEST.AMRCloud.Services.Scoping.Domain.Entities;

namespace TEST.AMRCloud.Services.Scoping.Infrastructure.Persistence;

/// <summary>
/// Primary EF Core DbContext for Scoping operations.
/// Manages Fund, Routine, FundRoutineMapping, Engagement, and ScopingDetail entities.
/// </summary>
public class ScopingContext : DbContext
{
    public ScopingContext(DbContextOptions<ScopingContext> options) : base(options)
    {
    }
    public DbSet<Fund> Funds => Set<Fund>();
    public DbSet<Engagement> Engagements => Set<Engagement>();
    public DbSet<ScopingDetail> ScopingDetails => Set<ScopingDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Fund entity
        modelBuilder.Entity<Fund>()
            .HasKey(f => f.Id);

        // Configure Routine entity

        // Configure Engagement entity
        modelBuilder.Entity<Engagement>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<Engagement>()
            .Property(e => e.EngagementName)
            .HasMaxLength(255);

        modelBuilder.Entity<Engagement>()
            .Property(e => e.EngagementCode)
            .HasMaxLength(50);

        modelBuilder.Entity<Engagement>()
            .Property(e => e.EngagementManager)
            .HasMaxLength(255);

        modelBuilder.Entity<Engagement>()
            .Property(e => e.EngagementPartner)
            .HasMaxLength(255);
                
        // Configure ScopingDetail entity
        modelBuilder.Entity<ScopingDetail>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<ScopingDetail>()
            .Property(s => s.Description)
            .HasMaxLength(500);

        modelBuilder.Entity<ScopingDetail>()
            .Property(s => s.EngagementManager)
            .HasMaxLength(255);
        modelBuilder.Entity<Fund>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FundCode).IsRequired().HasMaxLength(50);
            entity.Property(e => e.FundName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt);
        });

    }
}
