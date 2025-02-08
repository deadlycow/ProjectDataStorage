using Data.Entities;

namespace Data.Interfaces;
public interface IServiceRepository : IBaseRepository<ServiceEntity>
{
  public Task<List<ServiceEntity>> GetServicesByIdsAsync(List<int> serviceIds);
}
