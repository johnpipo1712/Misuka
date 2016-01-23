using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.OrderingDetails
{
  public class EditOrderingDetailCommand
  {
    public EditOrderingDetailCommand(Guid orderingDetailId, string productCode, string name
      , string brand, decimal price, long quantity, string note, string link
      , string linkUrl, string color, string size)
    {
      OrderingDetailId = orderingDetailId;
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
    public Guid OrderingDetailId { get; set; }
 
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
  }
}
