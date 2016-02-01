using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.OrderingDetails
{
  public class AddOrderingDetailCommand
  {
    public AddOrderingDetailCommand( string productCode, string name
      , string brand, decimal price, long quantity, string note, string link
      , string linkUrl, string color, string size, Guid orderingId)
    {
      OrderingId = orderingId;
      ProductCode = productCode;
      Name = name;
      Brand = brand;
      Price = price;
      Quantity = quantity;
      Note = note;
      Link = link;
      LinkUrl = linkUrl;
      Color = color;
      Size = size;
    }

    public string ProductCode { get; set; }

    public string Name { get; set; }

    public string Brand { get; set; }

    public decimal Price { get; set; }

    public long Quantity { get; set; }

    public string Note { get; set; }

    public string Link { get; set; }

    public string LinkUrl { get; set; }

    public string Color { get; set; }

    public string Size { get; set; }

    public Guid OrderingId { get; set; }
  }
}
