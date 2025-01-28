using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class ServiceRepository(ContextDb context) : BaseRepository<ServiceEntity>(context), IServiceRepository
{
}
