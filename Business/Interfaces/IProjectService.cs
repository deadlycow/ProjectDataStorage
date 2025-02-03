using Business.Dto;
using Business.Factories;
using Business.Models;
using System.Linq.Expressions;

namespace Business.Interfaces
{
  public interface IProjectService
  {
    public Task<IResult> CreateAsync(ProjectDto project);
    public Task<IResult> GetByExpressionAsync(string projectNumber);
    public Task<IResult> UpdateAsync(ProjectDto project);
    public Task<IResult> GetAllAsync();
    public Task<IResult> DeleteAsync(int id);
  }
}