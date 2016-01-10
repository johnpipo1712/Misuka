using System.Data.Entity;
using Misuka.Domain.Entity;
using Misuka.Infrastructure.EntityFramework;

namespace Misuka.Domain.Context
{
  public class MisukaDBContext : DataContext
  {
    static MisukaDBContext()
    {
      Database.SetInitializer<MisukaDBContext>(null);
    }

    public MisukaDBContext()
      : base("Name=Connection")
    {
      Configuration.AutoDetectChangesEnabled = false;
    }


    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
     
    }
  }
}

