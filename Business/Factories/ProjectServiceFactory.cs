using Business.Dto;
using Data.Entities;

namespace Business.Factories;
public static class ProjectServiceFactory
{
  public static ProjectServiceEntity Create(ProjectServiceDto dto) => new()
  {
    ProjectId = dto.ProjectId,
    ServiceId = dto.ServiceId,
  };
  public static IEnumerable<ProjectServiceEntity> Create(IEnumerable<ProjectServiceDto> dtos) => dtos.Select(Create).ToList();

  public static IEnumerable<ProjectServiceEntity> Create(int id, List<ProjectServiceDto> dtoList)
  {
    return dtoList.Select(dto => new ProjectServiceEntity
    {
      ProjectId = id,
      ServiceId = dto.ServiceId,
    }).ToList();
  }
  //public static IEnumerable<ProjectServiceEntity> Create(IEnumerable<ProjectServiceDto> dtos) => dtos.Select(Create).ToList();
}
