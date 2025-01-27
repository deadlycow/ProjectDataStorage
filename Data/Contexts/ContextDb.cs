using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Data.Contexts;
public class ContextDb(DbContextOptions<ContextDb> options) : DbContext(options)
{
  public DbSet<CustomerEntity> Customer { get; set; }
  public DbSet<ServiceEntity> ServiceType { get; set; }
  public DbSet<StatusEntity> StatusType { get; set; }
  public DbSet<ProjectEntity> Project { get; set; }
  public DbSet<EmployeeEntity> Employee { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<ProjectEntity>()
      .Property(e => e.Id)
      .UseIdentityColumn(100, 1);

    modelBuilder.Entity<ProjectEntity>()
      .Property(e => e.ProjectNumber)
      .HasComputedColumnSql("'P-' + Cast(Id as varchar)", stored: true);

    modelBuilder.Entity<ProjectEntity>()
      .HasOne(p => p.Customer)
      .WithMany(c => c.Project)
      .HasForeignKey(p => p.CustomerId)
      .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<ProjectEntity>()
      .HasOne(p => p.Employees)
      .WithMany(e => e.Project)
      .HasForeignKey(p => p.EmployeeId)
      .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<ProjectEntity>()
      .HasOne(p => p.StatusType)
      .WithMany(s => s.Project)
      .HasForeignKey(p => p.StatusTypeId)
      .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<ProjectEntity>()
      .HasMany(p => p.ServiceTypes)
      .WithMany(s => s.Project)
      .UsingEntity<Dictionary<string, object>>(
         "ProjectService",
         j => j.HasOne<ServiceEntity>().WithMany().HasForeignKey("ServiceId"),
         j => j.HasOne<ProjectEntity>().WithMany().HasForeignKey("ProjectId")
        );
  }
}


//modelBuilder.Entity<EmployeeEntity>().HasData(
//      new EmployeeEntity { Name = "Kungen" },
//      new EmployeeEntity { Name = "Andreas" },
//      new EmployeeEntity { Name = "Lärling" }
//      );

//modelBuilder.Entity<ServiceEntity>().HasData(
//  new ServiceEntity { Name = "Service 1", Price = 100 },
//  new ServiceEntity { Name = "Service 2", Price = 200 },
//  new ServiceEntity { Name = "Service 3", Price = 300 }
//);

//modelBuilder.Entity<StatusEntity>().HasData(
//  new StatusEntity { Status = "Ej Påbörjad" },
//  new StatusEntity { Status = "Påbörjad" },
//  new StatusEntity { Status = "Avslutad" }
//);

//.HasData(
//        new { ProjectId = 1, ServiceId = 1 },
//        new { ProjectId = 1, ServiceId = 2 },
//        new { ProjectId = 2, ServiceId = 2 },
//        new { ProjectId = 3, ServiceId = 3 },
//        new { ProjectId = 4, ServiceId = 1 },
//        new { ProjectId = 4, ServiceId = 3 },
//        new { ProjectId = 5, ServiceId = 2 }

//modelBuilder.Entity<CustomerEntity>().HasData(
//  new CustomerEntity { Name = "Kund 1" },
//  new CustomerEntity { Name = "Kund 2" },
//  new CustomerEntity { Name = "Kund 3" },
//  new CustomerEntity { Name = "Kund 4" }
//  );



//modelBuilder.Entity<ProjectEntity>().HasData(
//  new ProjectEntity { Name = "Projekt 1", StartDate = new DateOnly(2021, 1, 1), EndDate = new DateOnly(2121, 1, 31), CustomerId = 1, StatusTypeId = 1 },
//  new ProjectEntity { Name = "Projekt 2", StartDate = new DateOnly(2025, 1, 1), EndDate = new DateOnly(2121, 1, 31), CustomerId = 2, StatusTypeId = 1 },
//  new ProjectEntity { Name = "Projekt 3", StartDate = new DateOnly(2023, 1, 1), EndDate = new DateOnly(2121, 1, 31), CustomerId = 3, StatusTypeId = 2 },
//  new ProjectEntity { Name = "Projekt 4", StartDate = new DateOnly(2020, 1, 1), EndDate = null, CustomerId = 1, StatusTypeId = 2 },
//  new ProjectEntity { Name = "Projekt 5", StartDate = new DateOnly(2022, 1, 1), EndDate = new DateOnly(2121, 1, 31), CustomerId = 4, StatusTypeId = 3 }
//  );