using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class ProjectRepository(ContextDb context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
  public override async Task<IEnumerable<ProjectEntity>?> GetAllAsync(Func<IQueryable<ProjectEntity>, IQueryable<ProjectEntity>>? includeExpression = null)
  {
    try
    {
      var entities = await _context.Project
        .Include(p => p.Customer)
        .Include(p => p.Employees)
        .Include(p => p.StatusType)
        .ToListAsync();

      return entities;
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error fetching entities: {ex.Message}");
      return null;
    }
  }
}
