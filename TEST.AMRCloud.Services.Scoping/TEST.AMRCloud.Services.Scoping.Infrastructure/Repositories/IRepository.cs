using TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;

namespace TEST.AMRCloud.Services.Scoping.Infrastructure.Repositories;

/// <summary>
/// Generic repository interface for data access operations.
/// </summary>
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}
