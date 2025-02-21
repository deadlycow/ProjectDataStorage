using Business.Dto;
using Data.Entities;

namespace Business.Factories;
public static class CustomerFactory
{
  public static CustomerDto Create(CustomerEntity entity) => new()
  {
    Id = entity.Id,
    Name = entity.Name,
  };
  public static IEnumerable<CustomerDto> CreateList(IEnumerable<CustomerEntity> entities) => entities.Select(Create);
  public static CustomerEntity Create(CustomerDto entity) => new()
  {
    Name = entity.Name,
  };
  public static CustomerEntity Update(CustomerDto entity) => new()
  {
    Id = entity.Id,
    Name = entity.Name,
  };
}
