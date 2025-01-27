using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Contexts;

public class ContextDbFactory : IDesignTimeDbContextFactory<ContextDb>
{
  public ContextDb CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<ContextDb>();
    optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\ProjectDataStorage\\Data\\DataBase\\dbStorageProject.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");
    return new ContextDb(optionsBuilder.Options);
  }
}