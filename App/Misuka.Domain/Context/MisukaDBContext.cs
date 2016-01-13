using System.Data.Entity;
using Misuka.Domain.DTO;
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

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePerson> RolePersons { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Permission> Permission { get; set; }
    public DbSet<ContentMenu> ContentMenu { get; set; }
    public DbSet<ExchangeRate> ExchangeRate { get; set; }
    public DbSet<TypeMember> TypeMember { get; set; }
    public DbSet<Slider> Slider { get; set; }
    public DbSet<WebSiteLink> WebSiteLink { get; set; }
    public DbSet<Ordering> Ordering { get; set; }
    public DbSet<OrderingDetail> OrderingDetail { get; set; }
    public DbSet<OrderingHistory> OrderingHistory { get; set; }
  
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Configurations.Add(new RoleMap());
      modelBuilder.Configurations.Add(new UserMap());
      modelBuilder.Configurations.Add(new RolePersonMap());
      modelBuilder.Configurations.Add(new PersonMap());
      modelBuilder.Configurations.Add(new PermissionMap());
      modelBuilder.Configurations.Add(new RolePermissionMap());
      modelBuilder.Configurations.Add(new ContentMenuMap());
      modelBuilder.Configurations.Add(new ExchangeRateMap());
      modelBuilder.Configurations.Add(new TypeMemberMap());
      modelBuilder.Configurations.Add(new SliderMap());
      modelBuilder.Configurations.Add(new WebSiteLinkMap());
      modelBuilder.Configurations.Add(new OrderingMap());
      modelBuilder.Configurations.Add(new OrderingDetailMap());
      modelBuilder.Configurations.Add(new OrderingHistoryMap());
    }
  }
}

