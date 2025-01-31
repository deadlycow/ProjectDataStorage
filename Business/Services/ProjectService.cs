using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System.Linq.Expressions;

namespace Business.Services;
public class ProjectService(IProjectRepository repository) : IProjectService
{
  private readonly IProjectRepository _repository = repository;

  public Task<IResult> CreateAsync(ProjectDto project)
  {
    throw new NotImplementedException();
  }
  public Task<IResult> GetByExpressoinAsync(Expression<Func<ProjectDto, bool>> predicate)
  {
    throw new NotImplementedException();
  }
  public Task<IResult> UpdateAsync(ProjectDto project)
  {
    throw new NotImplementedException();
  }
  public async Task<IResult> GetAllAsync()
  {
    var projectEntity = await _repository.GetAllAsync();

    var projects = projectEntity?.Select(PressentationFactory.Create);

    return Result<IEnumerable<PressentationModel>>.Ok(projects);
  }
  public Task<IResult> DeleteAsync(int id)
  {
    throw new NotImplementedException();
  }
}
