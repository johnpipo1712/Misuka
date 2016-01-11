using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.DTO
{
  public class RolePermissionDTO
  {
    public Guid RolePermissionId { get; set; }
    public Guid RoleId { get; set; }
    public int PermissionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
  }
}
