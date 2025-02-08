using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ServiceRepository(ContextDb context) : BaseRepository<ServiceEntity>(context), IServiceRepository
{
  public async Task<List<ServiceEntity>> GetServicesByIdsAsync(List<int> serviceIds)
  {
    return await _context.ServiceType.Where(s => serviceIds.Contains(s.Id)).ToListAsync();
  }
}
