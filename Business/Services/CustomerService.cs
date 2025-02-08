using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Business.Services;
public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
  private readonly ICustomerRepository _customerRepository = customerRepository;

  public Task<IResult> CreateAsync(CustomerDto entity)
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
      var respons = await _customerRepository.GetAllAsync();
      if (respons == null || !respons.Any())
        return Result.BadRequest("No customers");

      var customers = CustomerFactory.CreateList(respons);

      return Result<IEnumerable<CustomerDto>>.Ok(customers);
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex);
      return Result.InternalServerError("Error occurred while fetching customers");
    }
  }

  public Task<IResult> GetAsync(string id)
  {
    throw new NotImplementedException();
  }

  public Task<IResult> UpdateAsync(string id, CustomerDto entity)
  {
    throw new NotImplementedException();
  }
}
