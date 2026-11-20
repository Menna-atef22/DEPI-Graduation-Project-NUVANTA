using System.Linq.Expressions;

namespace Nuvanta.Domain.Interfaces;

/// <summary>
/// Generic repository. Read methods (GetAll / Find / FirstOrDefault) return NO-TRACKING results.
/// To update an entity: load it with GetByIdAsync (tracked), change it, then call IUnitOfWork.SaveChangesAsync.
/// </summary>
public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<T?> GetByKeysAsync(object[] keyValues, CancellationToken ct = default);

    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);

    Task<IReadOnlyList<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken ct = default,
        params Expression<Func<T, object?>>[] includes);

    Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken ct = default,
        params Expression<Func<T, object?>>[] includes);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default);

    Task AddAsync(T entity, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);
    void Update(T entity);
    void Remove(T entity);
}
