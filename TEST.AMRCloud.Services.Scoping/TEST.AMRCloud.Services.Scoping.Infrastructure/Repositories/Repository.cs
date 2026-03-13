using Microsoft.EntityFrameworkCore;
using TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Persistence;

namespace TEST.AMRCloud.Services.Scoping.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation.
/// Provides common CRUD operations with EF Core.
/// Follows the project convention: Always use .AsNoTracking() for read-only queries.
/// </summary>
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly ScopingContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(ScopingContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public virtual async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
