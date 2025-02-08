using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;
public class ProjectServiceRepository(ContextDb context) : BaseRepository<ProjectServiceEntity>(context), IProjectServiceRepository
{
}
