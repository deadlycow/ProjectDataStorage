using Business.Dto;

namespace Business.Interfaces;
public interface IProjectServiceService : IBaseService<ProjectServiceDto, string>
{
  public Task<IResult> CreateList(int id, List<ProjectServiceDto> listDto);
  public Task<IResult> Remove(int id, List<ProjectServiceDto> listDto);
}