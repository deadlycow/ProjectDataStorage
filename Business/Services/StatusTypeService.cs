using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

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
    var response = await _statusRepository.GetAllAsync();
    if (response == null)
      return Result.BadRequest("No status found");
    
    var status = StatusTypeFactory.CreateList(response);
    return Result<IEnumerable<StatusDto>>.Ok(status);
  }

  public Task<IResult> GetAsync(string id)
  {
    throw new NotImplementedException();
  }

  public Task<IResult> UpdateAsync(string id, StatusDto entity)
  {
    throw new NotImplementedException();
  }
}
