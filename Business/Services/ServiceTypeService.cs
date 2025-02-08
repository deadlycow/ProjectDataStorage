using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;
public class ServiceTypeService(IServiceRepository serviceRepository) : IServiceTypeService
{
  private readonly IServiceRepository _serviceRepository = serviceRepository;

  public Task<IResult> CreateAsync(ServiceTypeDto entity)
  {
    throw new NotImplementedException();
  }

  public Task<IResult> DeleteAsync(string id)
  {
    throw new NotImplementedException();
  }

  public async Task<IResult> GetAllAsync()
  {
    var response = await _serviceRepository.GetAllAsync();
    if (response == null)
      return Result.BadRequest("No services found");
    var services = ServiceTypeFactory.CreateList(response);
    return Result<IEnumerable<ServiceTypeDto>>.Ok(services);
  }

  public Task<IResult> GetAsync(string id)
  {
    throw new NotImplementedException();
  }

  public Task<IResult> UpdateAsync(string id, ServiceTypeDto entity)
  {
    throw new NotImplementedException();
  }
}
