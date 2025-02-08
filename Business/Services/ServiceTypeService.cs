using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System.Diagnostics;

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
    try
    {
      var response = await _serviceRepository.GetAllAsync();
      if (response == null || !response.Any())
        return Result.BadRequest("No services found");

      var services = ServiceTypeFactory.CreateList(response);
      if (services == null)
        return Result.BadRequest("Failed to process services to dto");

      return Result<IEnumerable<ServiceTypeDto>>.Ok(services);
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex);
      return Result.InternalServerError("Error occurred while fetching services");
    }
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
