namespace Business.Interfaces;
public interface IBaseService<TDto, TKey> where TDto : class
{
  public Task<IResult> CreateAsync(TDto dto);
  public Task<IResult> GetAsync(TKey id);
  public Task<IResult> UpdateAsync(TDto dto);
  public Task<IResult> GetAllAsync();
  public Task<IResult> DeleteAsync(TKey id);
}