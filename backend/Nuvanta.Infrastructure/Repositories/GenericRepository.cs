using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nuvanta.Domain.Interfaces;
using Nuvanta.Infrastructure.Persistence;

namespace Nuvanta.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly NuvantaDbContext Db;
    protected readonly DbSet<T> Set;

    public GenericRepository(NuvantaDbContext db)
    {
        Db = db;
        Set = db.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        => await Set.FindAsync(new object[] { id }, ct);

    public async Task<T?> GetByKeysAsync(object[] keyValues, CancellationToken ct = default)
        => await Set.FindAsync(keyValues, ct);

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
        => await Set.AsNoTracking().ToListAsync(ct);

    public async Task<IReadOnlyList<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken ct = default,
        params Expression<Func<T, object?>>[] includes)
        => await Apply(includes).Where(predicate).ToListAsync(ct);

    public async Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken ct = default,
        params Expression<Func<T, object?>>[] includes)
        => await Apply(includes).FirstOrDefaultAsync(predicate, ct);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        => Set.AnyAsync(predicate, ct);

    public Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default)
        => predicate is null ? Set.CountAsync(ct) : Set.CountAsync(predicate, ct);

    public async Task AddAsync(T entity, CancellationToken ct = default) => await Set.AddAsync(entity, ct);
    public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default) => Set.AddRangeAsync(entities, ct);
    public void Update(T entity) => Set.Update(entity);
    public void Remove(T entity) => Set.Remove(entity);

    private IQueryable<T> Apply(Expression<Func<T, object?>>[] includes)
    {
        IQueryable<T> q = Set.AsNoTracking();
        foreach (var include in includes) q = q.Include(include);
        return q;
    }
}
