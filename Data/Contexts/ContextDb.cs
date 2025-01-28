using Data.Entities;
using Microsoft.EntityFrameworkCore;

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
    modelBuilder.Entity<CustomerEntity>()
      .HasIndex(c => c.Name)
      .IsUnique();

    modelBuilder.Entity<StatusEntity>()
      .HasIndex(c => c.Name)
      .IsUnique();

    modelBuilder.Entity<ServiceEntity>()
      .HasIndex(c => c.Name)
      .IsUnique();

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