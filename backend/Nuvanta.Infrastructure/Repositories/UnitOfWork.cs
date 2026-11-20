using Nuvanta.Domain.Interfaces;
using Nuvanta.Infrastructure.Persistence;

namespace Nuvanta.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly NuvantaDbContext _db;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(NuvantaDbContext db) => _db = db;

    public IGenericRepository<T> Repository<T>() where T : class
    {
        if (!_repositories.TryGetValue(typeof(T), out var repo))
        {
            repo = new GenericRepository<T>(_db);
            _repositories[typeof(T)] = repo;
        }
        return (IGenericRepository<T>)repo;
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
}
