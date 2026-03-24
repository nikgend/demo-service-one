using Microsoft.EntityFrameworkCore;
using TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;
using TEST.AMRCloud.Services.Scoping.Domain.Entities;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Persistence;

namespace TEST.AMRCloud.Services.Scoping.Infrastructure.Repositories;


/// <summary>
/// Engagement data access repository.
/// </summary>
public interface IEngagementRepository : IRepository<Engagement>
{
    Task<Engagement?> GetEngagementByCodeAsync(string engagementCode);
    Task<IEnumerable<Engagement>> GetEngagementsByEngagementManagerAsync(string engagementManager);
    Task<Engagement?> GetEngagementWithFundsAsync(int engagementId);
}

public class EngagementRepository : Repository<Engagement>, IEngagementRepository
{
    public EngagementRepository(ScopingContext context) : base(context)
    {
    }

    public async Task<Engagement?> GetEngagementByCodeAsync(string engagementCode)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EngagementCode == engagementCode);
    }

    public async Task<IEnumerable<Engagement>> GetEngagementsByEngagementManagerAsync(string engagementManager)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(e => e.EngagementManager == engagementManager)
            .ToListAsync();
    }

    public async Task<Engagement?> GetEngagementWithFundsAsync(int engagementId)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == engagementId);
    }
}
