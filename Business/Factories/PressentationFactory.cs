using Business.Models;
using Data.Entities;

namespace Business.Factories;
public static class PressentationFactory
{
  public static PressentationModel Create(ProjectEntity entity)
  {
    return new PressentationModel()
    {
      ProjectNumber = entity.ProjectNumber,
      Name = entity.Name,
      StartDate = entity.StartDate,
      EndDate = entity.EndDate,
      Status = entity.StatusType.Name
    };
  }
}
