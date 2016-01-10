using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.Entity
{
  public class RolePermission : Misuka.Infrastructure.EntityFramework.Entity
  {
    public Guid RolePermissionId { get; set; }
    public Guid RoleId { get; set; }
    public int  PermissionId { get; set; }
  }
  public class RolePermissionMap : EntityTypeConfiguration<RolePermission>
  {
    public RolePermissionMap()
    {
      this.HasKey(t => t.RolePermissionId);
      this.ToTable("[dbo].[RolePermission]");
    }
  }
}
