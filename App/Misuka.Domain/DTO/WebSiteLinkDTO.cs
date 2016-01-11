using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.DTO
{
  public class WebSiteLinkDTO
  {
    public Guid WebSiteLinkId { get; set; }

    public string Name { get; set; }

    public string Link { get; set; }

    public string ImageUrl { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public string CreatedByName { get; set; }
  }
}
