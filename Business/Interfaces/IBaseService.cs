namespace Business.Interfaces;
public interface IBaseService<TEntity, TKey> where TEntity : class
{
  public Task<IResult> CreateAsync(TEntity entity);
  public Task<IResult> GetAsync(TKey id);
  public Task<IResult> UpdateAsync(TKey id, TEntity entity);
  public Task<IResult> GetAllAsync();
  public Task<IResult> DeleteAsync(TKey id);
}
