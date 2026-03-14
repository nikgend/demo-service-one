using Microsoft.EntityFrameworkCore;
using TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Persistence;

namespace TEST.AMRCloud.Services.Scoping.Infrastructure.Repositories;

/// <summary>
/// Fund data access repository.
/// Provides specialized queries for Fund operations.
/// </summary>
public interface IFundRepository : IRepository<Fund>
{
    Task<Fund?> GetFundByCodeAsync(string fundCode);
    Task<IEnumerable<Fund>> GetFundsByEngagementManagerAsync(string engagementManager);
    Task<IEnumerable<Fund>> GetFundsByEngagementAsync(int engagementId);
}

public class FundRepository : Repository<Fund>, IFundRepository
{
    public FundRepository(ScopingContext context) : base(context)
    {
    }

    public async Task<Fund?> GetFundByCodeAsync(string fundCode)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.FundCode == fundCode);
    }

    public async Task<IEnumerable<Fund>> GetFundsByEngagementManagerAsync(string engagementManager)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(f => f.EngagementManager == engagementManager)
            .ToListAsync();
    }

    public async Task<IEnumerable<Fund>> GetFundsByEngagementAsync(int engagementId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(f => f.EngagementId == engagementId)
            .ToListAsync();
    }
}

/// <summary>
/// Routine data access repository.
/// </summary>
public interface IRoutineRepository : IRepository<Routine>
{
    Task<Routine?> GetRoutineByCodeAsync(string routineCode);
    Task<IEnumerable<Routine>> GetRoutinesByEngagementManagerAsync(string engagementManager);
}

public class RoutineRepository : Repository<Routine>, IRoutineRepository
{
    public RoutineRepository(ScopingContext context) : base(context)
    {
    }

    public async Task<Routine?> GetRoutineByCodeAsync(string routineCode)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RoutineCode == routineCode);
    }

    public async Task<IEnumerable<Routine>> GetRoutinesByEngagementManagerAsync(string engagementManager)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(r => r.EngagementManager == engagementManager)
            .ToListAsync();
    }
}

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
            .Include(e => e.Funds)
            .FirstOrDefaultAsync(e => e.Id == engagementId);
    }
}
