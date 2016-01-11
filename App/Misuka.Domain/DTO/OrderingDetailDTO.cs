using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.DTO
{
  public class OrderingDetailDTO
  {
    public Guid OrderingDetailId { get; set; }

    public string OrderingDetailCode { get; set; }

    public string ProductCode { get; set; }

    public string Name { get; set; }

    public string Brand { get; set; }

    public decimal? Price { get; set; }

    public long? Quantity { get; set; }

    public string Note { get; set; }

    public string Link { get; set; }

    public string LinkUrl { get; set; }

    public string Color { get; set; }

    public string Size { get; set; }
  }
}
