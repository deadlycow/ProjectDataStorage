using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;
public class CustomerRepository(ContextDb context) : BaseRepository<CustomerEntity>(context), ICustomerRepository
{
}
