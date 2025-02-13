using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using System.Diagnostics;

namespace Data.Repositories;
public class ProjectServiceRepository(ContextDb context) : BaseRepository<ProjectServiceEntity>(context), IProjectServiceRepository
{
  public async Task<bool> CreateListAsync(IEnumerable<ProjectServiceEntity> list)
  {
		try
		{
			await _dbSet.AddRangeAsync(list);
			 var result = await _context.SaveChangesAsync();
			return result > 0;
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Error addin list entities: {ex.Message}");
			return false;
		}
  }
  public async Task<bool> RemoveListAsync(IEnumerable<ProjectServiceEntity> list)
  {
    try
    {
      _dbSet.RemoveRange(list);
      return await _context.SaveChangesAsync() > 0;
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error deleting list entities: {ex.Message}");
      return false;
    }
  }
}
