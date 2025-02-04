using Business.Dto;
using Data.Interfaces;

namespace Business.Interfaces;
public interface ICustomerService : IBaseService<CustomerDto,string>
{
}
