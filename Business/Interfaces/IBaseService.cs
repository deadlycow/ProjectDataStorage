namespace Business.Interfaces;
public interface IBaseService<TDto, TKey> where TDto : class
{
  public Task<IResult> CreateAsync(TDto entity);
  public Task<IResult> GetAsync(TKey id);
  public Task<IResult> UpdateAsync(TDto entity);
  public Task<IResult> GetAllAsync();
  public Task<IResult> DeleteAsync(TKey id);
}