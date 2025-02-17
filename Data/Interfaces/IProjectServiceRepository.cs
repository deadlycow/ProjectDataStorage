using Data.Entities;

namespace Data.Interfaces;
public interface IProjectServiceRepository : IBaseRepository<ProjectServiceEntity>
{
  public void CreateListAsync(IEnumerable<ProjectServiceEntity> list);
  public void RemoveList(IEnumerable<ProjectServiceEntity> list);
}