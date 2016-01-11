using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.DTO
{
  public class ContentMenuDTO
  {
    public Guid ContentMenuId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string MetaKeywork { get; set; }

    public string MetaDescription { get; set; }

    public string Image { get; set; }
  }
}
