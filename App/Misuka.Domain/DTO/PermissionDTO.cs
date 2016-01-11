
using System.Collections.Generic;
using System.Dynamic;


namespace Misuka.Domain.DTO
{
  public class PermissionDTO
  {
    public int PermissionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PermissionGroup { get; set; }
    public List<PermissionDTO> ListPermissions { get; set; } 
    public string id { get; set; }
    public string text { get; set; }
    public bool expanded { get; set; }
    public bool @checked { get; set; }

    public string MasterGroup { get; set; }
    public List<PermissionDTO> items { get; set; }
  }
}
