using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.Scoping.Domain.AggregateRoots.EngagementAggregate;
using Services.Scoping.Domain.Common;
using Services.Scoping.Domain.Contracts;
using Services.Scoping.Domain.Contracts.Engagement;
using Services.Scoping.Domain.Entities;
using Services.Scoping.Domain.Entities.engagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Scoping.Infrastructure.Persistence
{
    public class ScopingDbContext : DbContext, IUnitOfWork
    {
        public ScopingDbContext(DbContextOptions<ScopingDbContext> options) : base(options) { }

        public DbSet<Engagement> Engagement { get; set; }
        public virtual DbSet<EngagementTypes> EngagementType { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Fund> Fund { get; set; }
        public virtual DbSet<FundType> FundType { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Engagement>()
                    .ToTable("Engagement", "Metadata");

            modelBuilder.Entity<Region>()
            .ToTable("Region", "Lookup");

            modelBuilder.Entity<EngagementTypes>()
                    .ToTable("EngagementType", "Lookup");

            modelBuilder.Entity<Fund>()
                    .ToTable("Fund", "Metadata");
            modelBuilder.Entity<FundType>()
                    .ToTable("FundType", "Lookup");

            base.OnModelCreating(modelBuilder);

        }

        public async Task<int> SaveEngagementEntitiesAsync(CreateEngagementRequest request, CancellationToken cancellationToken = default)
        {
            var engagement = new Engagement
            {
                EngagementName = request.Name,
                RegionId = request.RegionId,
                EngagementTypeId = request.EngagementTypeId 
            };
            await Engagement.AddAsync(engagement, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return engagement.EngagementId;
        }

        public async Task<bool> UpdateEngagementEntitiesAsync(UpdateEngagementRequest request, CancellationToken cancellationToken = default)
        {
            var engagement = await Engagement.FirstOrDefaultAsync(e => e.EngagementId == request.EngagementId, cancellationToken);
            if (engagement == null)
            {
                return false;
            }

            engagement.EngagementName = request.Name;
            engagement.EngagementTypeId = request.EngagementTypeId;

            await SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<int> SaveFundEntitiesAsync(CreateFundRequest createFundRequest, CancellationToken cancellationToken = default)
        {
            var fund = new Fund
            {
                FundName = createFundRequest.Name,
                EngagementId = createFundRequest.EngagementId,
                FundTypeId = createFundRequest.FundTypeId,
                PeriodStartDate = createFundRequest.PeriodStartDate,
                PeriodEndDate = createFundRequest.PeriodEndDate
            };
            await Fund.AddAsync(fund, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return fund.FundId;
        }
    }
}
