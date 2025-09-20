using System.Linq.Expressions;

namespace Domain.Common.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    void Update(TEntity entity);
    
    void Remove(TEntity entity);
}