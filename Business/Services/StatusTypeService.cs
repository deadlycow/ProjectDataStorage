using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services;
public class StatusTypeService(IStatusRepository statusRepository) : IStatusTypeService
{
  private readonly IStatusRepository _statusRepository = statusRepository;

  public Task<IResult> CreateAsync(StatusDto entity)
  {
    throw new NotImplementedException();
  }

  public Task<IResult> DeleteAsync(string id)
  {
    throw new NotImplementedException();
  }

  public async Task<IResult> GetAllAsync()
  {
    try
    {
      var response = await _statusRepository.GetAllAsync();
      if (response != null && response.Any())
      {
        var status = StatusTypeFactory.CreateList(response);
        return Result<IEnumerable<StatusDto>>.Ok(status);
      }
      return Result.BadRequest("No status found");
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex);
      return Result.InternalServerError("An unexpected error occurred fetching status");
    }
  }

  public Task<IResult> GetAsync(string id)
  {
    throw new NotImplementedException();
  }

  public Task<IResult> UpdateAsync(StatusDto entity)
  {
    throw new NotImplementedException();
  }
}
