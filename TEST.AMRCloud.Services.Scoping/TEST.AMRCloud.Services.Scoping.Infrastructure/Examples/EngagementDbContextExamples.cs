using TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TEST.AMRCloud.Services.Scoping.Infrastructure.Examples;

/// <summary>
/// Example usage of ScopingContext for Engagement CRUD operations.
/// This demonstrates how to interact with the database using DbContext.
/// </summary>
public class EngagementDbContextExamples
{
    private readonly ScopingContext _context;

    public EngagementDbContextExamples(ScopingContext context)
    {
        _context = context;
    }

    // =====================================================================
    // CREATE OPERATIONS
    // =====================================================================

    /// <summary>
    /// Example: Add a new engagement to the database.
    /// </summary>
    public async Task<Engagement?> CreateEngagementExample()
    {
        var engagement = new Engagement
        {
            EngagementName = "ABC Manufacturing Audit 2024",
            EngagementCode = "ENG-2024-001",
            ClientName = "ABC Manufacturing Ltd",
            Status = "Active",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(6),
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "john.doe@company.com",
            IsActive = true
        };

        // Add to DbSet
        _context.Engagements.Add(engagement);

        // Save changes to database
        await _context.SaveChangesAsync();

        return engagement;
    }

    /// <summary>
    /// Example: Add engagement with related funds (one-to-many relationship).
    /// </summary>
    public async Task CreateEngagementWithFundsExample()
    {
        var engagement = new Engagement
        {
            EngagementName = "Retail Audit",
            EngagementCode = "ENG-2024-002",
            ClientName = "XYZ Retail Corp",
            Status = "Active",
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "admin@company.com",
            IsActive = true,
            Funds = new List<Fund>
            {
                new Fund
                {
                    FundName = "Operating Fund",
                    FundCode = "FND-001",
                    Status = "Active",
                    Amount = 500000,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "admin@company.com"
                },
                new Fund
                {
                    FundName = "Capital Fund",
                    FundCode = "FND-002",
                    Status = "Active",
                    Amount = 250000,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "admin@company.com"
                }
            }
        };

        _context.Engagements.Add(engagement);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Example: Bulk insert multiple engagements.
    /// </summary>
    public async Task BulkCreateEngagementsExample()
    {
        var engagements = new List<Engagement>
        {
            new Engagement
            {
                EngagementName = "Finance Review",
                EngagementCode = "ENG-2024-003",
                ClientName = "DEF Finance Inc",
                Status = "Planned",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "admin@company.com"
            },
            new Engagement
            {
                EngagementName = "IT Audit",
                EngagementCode = "ENG-2024-004",
                ClientName = "GHI Technology Ltd",
                Status = "Planned",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "admin@company.com"
            }
        };

        _context.Engagements.AddRange(engagements);
        await _context.SaveChangesAsync();
    }

    // =====================================================================
    // READ OPERATIONS
    // =====================================================================

    /// <summary>
    /// Example: Get all active engagements.
    /// Uses .AsNoTracking() for improved performance on read-only queries.
    /// </summary>
    public async Task<List<Engagement>> GetAllActiveEngagementsExample()
    {
        var engagements = await _context.Engagements
            .AsNoTracking()
            .Where(e => e.IsActive)
            .OrderByDescending(e => e.CreatedDate)
            .ToListAsync();

        return engagements;
    }

    /// <summary>
    /// Example: Get engagement by ID.
    /// </summary>
    public async Task<Engagement?> GetEngagementByIdExample(int engagementId)
    {
        var engagement = await _context.Engagements
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == engagementId && e.IsActive);

        return engagement;
    }

    /// <summary>
    /// Example: Get engagement by code (unique lookup).
    /// </summary>
    public async Task<Engagement?> GetEngagementByCodeExample(string engagementCode)
    {
        var engagement = await _context.Engagements
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EngagementCode == engagementCode && e.IsActive);

        return engagement;
    }

    /// <summary>
    /// Example: Get engagements by status with pagination.
    /// </summary>
    public async Task<List<Engagement>> GetEngagementsByStatusPaginatedExample(
        string status, int pageNumber = 1, int pageSize = 10)
    {
        var engagements = await _context.Engagements
            .AsNoTracking()
            .Where(e => e.Status == status && e.IsActive)
            .OrderByDescending(e => e.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return engagements;
    }

    /// <summary>
    /// Example: Get engagement with all related funds (eager loading).
    /// </summary>
    public async Task<Engagement?> GetEngagementWithFundsExample(int engagementId)
    {
        var engagement = await _context.Engagements
            .AsNoTracking()
            .Include(e => e.Funds) // Eager load related funds
            .FirstOrDefaultAsync(e => e.Id == engagementId && e.IsActive);

        return engagement;
    }

    /// <summary>
    /// Example: Get engagement with funds that have scoping details.
    /// </summary>
    public async Task<Engagement?> GetEngagementWithFundsAndScopingDetailsExample(int engagementId)
    {
        var engagement = await _context.Engagements
            .AsNoTracking()
            .Include(e => e.Funds)
                .ThenInclude(f => f.ScopingDetails) // Nested include
            .FirstOrDefaultAsync(e => e.Id == engagementId && e.IsActive);

        return engagement;
    }

    /// <summary>
    /// Example: Complex query with multiple filters and sorting.
    /// </summary>
    public async Task<List<Engagement>> GetEngagementsWithComplexFilterExample(
        string? clientName, string? status, DateTime? startDateFrom, DateTime? startDateTo)
    {
        var query = _context.Engagements.AsNoTracking();

        // Apply filters conditionally
        if (!string.IsNullOrEmpty(clientName))
            query = query.Where(e => e.ClientName!.Contains(clientName));

        if (!string.IsNullOrEmpty(status))
            query = query.Where(e => e.Status == status);

        if (startDateFrom.HasValue)
            query = query.Where(e => e.StartDate >= startDateFrom);

        if (startDateTo.HasValue)
            query = query.Where(e => e.StartDate <= startDateTo);

        // Apply default filters
        query = query.Where(e => e.IsActive);

        // Order and execute
        var engagements = await query
            .OrderByDescending(e => e.CreatedDate)
            .ToListAsync();

        return engagements;
    }

    /// <summary>
    /// Example: Get count of engagements by status.
    /// </summary>
    public async Task<Dictionary<string, int>> GetEngagementCountByStatusExample()
    {
        var counts = await _context.Engagements
            .AsNoTracking()
            .Where(e => e.IsActive)
            .GroupBy(e => e.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Status ?? "Unknown", x => x.Count);

        return counts;
    }

    // =====================================================================
    // UPDATE OPERATIONS
    // =====================================================================

    /// <summary>
    /// Example: Update an existing engagement with tracking.
    /// </summary>
    public async Task<Engagement?> UpdateEngagementExample(
        int engagementId, string newName, string newStatus, string modifiedBy)
    {
        // Get entity with change tracking (no AsNoTracking)
        var engagement = await _context.Engagements
            .FirstOrDefaultAsync(e => e.Id == engagementId);

        if (engagement == null)
            return null;

        // Modify properties
        engagement.EngagementName = newName;
        engagement.Status = newStatus;
        engagement.ModifiedDate = DateTime.UtcNow;
        engagement.ModifiedBy = modifiedBy;

        // Save changes
        _context.Engagements.Update(engagement);
        await _context.SaveChangesAsync();

        return engagement;
    }

    /// <summary>
    /// Example: Partial update using direct SQL (for performance with many fields).
    /// </summary>
    public async Task<int> UpdateEngagementStatusDirectlyExample(
        int engagementId, string newStatus, string modifiedBy)
    {
        var rowsAffected = await _context.Engagements
            .Where(e => e.Id == engagementId)
            .ExecuteUpdateAsync(s => s
                .Set(e => e.Status, newStatus)
                .Set(e => e.ModifiedDate, DateTime.UtcNow)
                .Set(e => e.ModifiedBy, modifiedBy)
            );

        return rowsAffected;
    }

    /// <summary>
    /// Example: Soft delete (mark as inactive instead of hard delete).
    /// </summary>
    public async Task<int> SoftDeleteEngagementExample(int engagementId, string modifiedBy)
    {
        var rowsAffected = await _context.Engagements
            .Where(e => e.Id == engagementId)
            .ExecuteUpdateAsync(s => s
                .Set(e => e.IsActive, false)
                .Set(e => e.ModifiedDate, DateTime.UtcNow)
                .Set(e => e.ModifiedBy, modifiedBy)
            );

        return rowsAffected;
    }

    /// <summary>
    /// Example: Bulk update multiple engagements.
    /// </summary>
    public async Task<int> BulkUpdateEngagementStatusExample(
        List<int> engagementIds, string newStatus, string modifiedBy)
    {
        var rowsAffected = await _context.Engagements
            .Where(e => engagementIds.Contains(e.Id))
            .ExecuteUpdateAsync(s => s
                .Set(e => e.Status, newStatus)
                .Set(e => e.ModifiedDate, DateTime.UtcNow)
                .Set(e => e.ModifiedBy, modifiedBy)
            );

        return rowsAffected;
    }

    // =====================================================================
    // DELETE OPERATIONS
    // =====================================================================

    /// <summary>
    /// Example: Hard delete an engagement (permanent deletion).
    /// </summary>
    public async Task<bool> DeleteEngagementExample(int engagementId)
    {
        var engagement = await _context.Engagements
            .FirstOrDefaultAsync(e => e.Id == engagementId);

        if (engagement == null)
            return false;

        _context.Engagements.Remove(engagement);
        await _context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Example: Hard delete using ExecuteDeleteAsync (more efficient for single entity).
    /// </summary>
    public async Task<int> DeleteEngagementDirectlyExample(int engagementId)
    {
        var rowsAffected = await _context.Engagements
            .Where(e => e.Id == engagementId)
            .ExecuteDeleteAsync();

        return rowsAffected;
    }

    /// <summary>
    /// Example: Bulk hard delete multiple engagements.
    /// WARNING: This will cascade delete related funds and scoping details!
    /// </summary>
    public async Task<int> BulkDeleteEngagementsExample(List<int> engagementIds)
    {
        var rowsAffected = await _context.Engagements
            .Where(e => engagementIds.Contains(e.Id))
            .ExecuteDeleteAsync();

        return rowsAffected;
    }

    // =====================================================================
    // ADVANCED PATTERNS
    // =====================================================================

    /// <summary>
    /// Example: Using transactions for multiple operations.
    /// </summary>
    public async Task CreateEngagementWithTransactionExample(
        Engagement engagement, List<Fund> funds)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Create engagement
                _context.Engagements.Add(engagement);
                await _context.SaveChangesAsync();

                // Add funds for this engagement
                foreach (var fund in funds)
                {
                    fund.EngagementId = engagement.Id;
                }
                _context.Funds.AddRange(funds);
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // Rollback on error
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    /// <summary>
    /// Example: Raw SQL query for complex reporting.
    /// </summary>
    public async Task<List<dynamic>> GetEngagementReportExample()
    {
        var sql = @"
            SELECT
                e.Id,
                e.EngagementCode,
                e.EngagementName,
                e.ClientName,
                e.Status,
                COUNT(f.Id) as FundCount,
                SUM(f.Amount) as TotalAmount
            FROM Engagements e
            LEFT JOIN Funds f ON e.Id = f.EngagementId
            WHERE e.IsActive = 1
            GROUP BY e.Id, e.EngagementCode, e.EngagementName, e.ClientName, e.Status
            ORDER BY e.CreatedDate DESC";

        var result = await _context.Database.SqlQueryRaw<dynamic>(sql).ToListAsync();
        return result;
    }

    /// <summary>
    /// Example: Using FromSqlInterpolated for parameterized queries (safe from SQL injection).
    /// </summary>
    public async Task<List<Engagement>> GetEngagementByClientNameExample(string clientName)
    {
        var engagements = await _context.Engagements
            .FromSqlInterpolated($@"
                SELECT * FROM Engagements
                WHERE ClientName LIKE {'%' + clientName + '%'} AND IsActive = 1")
            .ToListAsync();

        return engagements;
    }
}
