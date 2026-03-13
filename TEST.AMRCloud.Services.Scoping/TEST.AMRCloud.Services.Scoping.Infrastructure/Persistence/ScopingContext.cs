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
    public DbSet<Routine> Routines => Set<Routine>();
    public DbSet<FundRoutineMapping> FundRoutineMappings => Set<FundRoutineMapping>();
    public DbSet<Engagement> Engagements => Set<Engagement>();
    public DbSet<ScopingDetail> ScopingDetails => Set<ScopingDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Fund entity
        modelBuilder.Entity<Fund>()
            .HasKey(f => f.Id);

        modelBuilder.Entity<Fund>()
            .Property(f => f.FundName)
            .HasMaxLength(255);

        modelBuilder.Entity<Fund>()
            .Property(f => f.FundCode)
            .HasMaxLength(50);

        modelBuilder.Entity<Fund>()
            .Property(f => f.Status)
            .HasMaxLength(50);

        // Configure Routine entity
        modelBuilder.Entity<Routine>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<Routine>()
            .Property(r => r.RoutineName)
            .HasMaxLength(255);

        modelBuilder.Entity<Routine>()
            .Property(r => r.RoutineCode)
            .HasMaxLength(50);

        modelBuilder.Entity<Routine>()
            .Property(r => r.Status)
            .HasMaxLength(50);

        // Configure FundRoutineMapping entity
        modelBuilder.Entity<FundRoutineMapping>()
            .HasKey(frm => frm.Id);

        modelBuilder.Entity<FundRoutineMapping>()
            .HasOne(frm => frm.Fund)
            .WithMany(f => f.RoutineMappings)
            .HasForeignKey(frm => frm.FundId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FundRoutineMapping>()
            .HasOne(frm => frm.Routine)
            .WithMany(r => r.FundMappings)
            .HasForeignKey(frm => frm.RoutineId)
            .OnDelete(DeleteBehavior.Cascade);

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
            .Property(e => e.ClientName)
            .HasMaxLength(255);

        modelBuilder.Entity<Engagement>()
            .Property(e => e.Status)
            .HasMaxLength(50);

        modelBuilder.Entity<Engagement>()
            .HasMany(e => e.Funds)
            .WithOne()
            .HasForeignKey(f => f.EngagementId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure ScopingDetail entity
        modelBuilder.Entity<ScopingDetail>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<ScopingDetail>()
            .HasOne(s => s.Fund)
            .WithMany(f => f.ScopingDetails)
            .HasForeignKey(s => s.FundId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ScopingDetail>()
            .Property(s => s.Description)
            .HasMaxLength(500);

        modelBuilder.Entity<ScopingDetail>()
            .Property(s => s.Status)
            .HasMaxLength(50);
    }
}
