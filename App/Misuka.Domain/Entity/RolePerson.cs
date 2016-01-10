using System;
using System.Data.Entity.ModelConfiguration;

namespace Misuka.Domain.Entity
{
  public class RolePerson : Misuka.Infrastructure.EntityFramework.Entity
  {
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Guid PersonId { get; set; }
  }

  public class RolePersonMap : EntityTypeConfiguration<RolePerson>
  {
    public RolePersonMap()
    {
      this.HasKey(t => t.RoleId);
      this.ToTable("[dbo].[RolePerson]");
    }
  }
}