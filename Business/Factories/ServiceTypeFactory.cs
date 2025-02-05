using Business.Dto;
using Data.Entities;

namespace Business.Factories;
public static class ServiceTypeFactory
{
  public static ServiceTypeDto Create(ServiceEntity entity) => new()
  {
    Id = entity.Id,
    Name = entity.Name,
    Price = entity.Price,
  };

  public static IEnumerable<ServiceTypeDto> CreateList(IEnumerable<ServiceEntity> services) => services.Select(Create);
}
