using Business.Dto;
using System.Linq.Expressions;

namespace Business.Interfaces;
public interface IProjectService
{
  Task<IResult> CreateAsync(ProjectDto project);
  Task<IResult> UpdateAsync(ProjectDto project);
  Task<IResult> GetByExpressoinAsync(Expression<Func<ProjectDto, bool>> predicate);
  Task<IResult> GetAllAsync();
  Task<IResult> DeleteAsync(int id);
}
