namespace Nuvanta.Domain.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<T> Repository<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
