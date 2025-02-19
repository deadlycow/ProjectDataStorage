using Business.Dto;

namespace Business.Interfaces
{
  public interface IServiceTypeService : IBaseService<ServiceTypeDto, string>
  {
    Task<IResult> DeleteAsync(int id);
  }
}