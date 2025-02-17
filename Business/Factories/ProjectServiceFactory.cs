using Business.Dto;
using Data.Entities;

namespace Business.Factories;
public static class ProjectServiceFactory
{
  public static IEnumerable<ProjectServiceEntity> Create(int id, List<ProjectServiceDto> dtoList)
  {
    return dtoList.Select(dto => new ProjectServiceEntity
    {
      ProjectId = id,
      ServiceId = dto.ServiceId,
    }).ToList();
  }
}
