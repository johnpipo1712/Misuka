using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Misuka.Domain.Entity
{
  public class Role : Misuka.Infrastructure.EntityFramework.Entity
  {
    public Guid RoleId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Type { get; set; }
  }

  public class RoleMap : EntityTypeConfiguration<Role>
  {
    public RoleMap()
    {
      this.HasKey(t => t.RoleId);
      this.ToTable("[dbo].[Role]");
    }
  }
}
