using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;
public class ProjectRepository(ContextDb context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
}