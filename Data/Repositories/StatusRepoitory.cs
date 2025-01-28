using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class StatusRepoitory(ContextDb context) : BaseRepository<StatusEntity>(context), IStatusRepository
{
}