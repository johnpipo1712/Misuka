using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.Entity
{
  public class Permission : Misuka.Infrastructure.EntityFramework.Entity
  {
    public int PermissionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PermissionGroup { get; set; }

  }
  public class PermissionMap : EntityTypeConfiguration<Permission>
  {
    public PermissionMap()
    {
      this.HasKey(t => t.PermissionId);
      this.ToTable("[dbo].[Permission]");
    }
  }
}
