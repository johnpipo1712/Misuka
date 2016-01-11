using System;
using System.Collections.Generic;
using Misuka.Domain.Enum;

namespace Misuka.Domain.DTO
{
  public class RoleDTO
  {
    public Guid RoleId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Type { get; set; }
    public bool Assigned
    {
      get; set;
    }
    
  }
}
