using Business.Dto;
using Business.Models;

namespace Business.Interfaces;
public interface IProjectService
{
  public Task<IResult> CreateAsync(ProjectDto project);
  public Task<IResult> GetByExpressionAsync(string projectNumber);
  public Task<IResult> UpdateAsync(string projectNumber, PressentationDetailsModel project);
  public Task<IResult> GetAllAsync();
  public Task<IResult> DeleteAsync(int id);
}