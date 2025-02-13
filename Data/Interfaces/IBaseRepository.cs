using System.Linq.Expressions;

namespace Data.Interfaces;
public interface IBaseRepository<TEntity> where TEntity : class
{
  Task<TEntity?> CreateAsync(TEntity entity);
  Task<IEnumerable<TEntity>?> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null);
  Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null);
  Task<bool> UpdateAsync(TEntity entity);
  Task<bool> DeleteAsync(TEntity entity);
  Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
}
