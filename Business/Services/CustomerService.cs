using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

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
    var respons = await _customerRepository.GetAllAsync();
    if (respons == null)
      return Result.BadRequest("No customers");
    var customers = CustomerFactory.CreateList(respons);

    return Result<IEnumerable<CustomerDto>>.Ok(customers);
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
