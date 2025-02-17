using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using System.Diagnostics;

namespace Data.Repositories;
public class ProjectServiceRepository(ContextDb context) : BaseRepository<ProjectServiceEntity>(context), IProjectServiceRepository
{
  public async void CreateListAsync(IEnumerable<ProjectServiceEntity> list)
  {
		try
		{
			await _dbSet.AddRangeAsync(list);
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Error addin list entities: {ex.Message}");
		}
  }
  public void RemoveList(IEnumerable<ProjectServiceEntity> list)
  {
    try
    {
      _dbSet.RemoveRange(list);
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error deleting list entities: {ex.Message}");
    }
  }
}
