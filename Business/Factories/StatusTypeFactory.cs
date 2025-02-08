using Business.Dto;
using Data.Entities;

namespace Business.Factories;
public static class StatusTypeFactory
{
  public static StatusDto Create(StatusEntity entity) => new()
  {
    Id = entity.Id,
    Name = entity.Name,
  };

  public static IEnumerable<StatusDto> CreateList(IEnumerable<StatusEntity> entities) => entities.Select(Create);
}
