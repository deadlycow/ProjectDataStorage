using Data.Entities;

namespace Data.Interfaces;
public interface IProjectServiceRepository : IBaseRepository<ProjectServiceEntity>
{
  public Task<bool> CreateListAsync(IEnumerable<ProjectServiceEntity> list);
  public Task<bool> RemoveListAsync(IEnumerable<ProjectServiceEntity> list);
}