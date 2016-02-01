using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class OrderingDetailModel
  {
    [Display(Name = "OrderingDetailId", ResourceType = typeof(Resources.OrderingDetail))]
    public Guid OrderingDetailId { get; set; }

    [Display(Name = "OrderingDetailCode", ResourceType = typeof(Resources.OrderingDetail))]
    public string OrderingDetailCode { get; set; }

    [Display(Name = "ProductCode", ResourceType = typeof(Resources.OrderingDetail))]
    public string ProductCode { get; set; }

    [Display(Name = "Name", ResourceType = typeof(Resources.OrderingDetail))]
    public string Name { get; set; }

    [Display(Name = "Brand", ResourceType = typeof(Resources.OrderingDetail))]
    public string Brand { get; set; }

    [Display(Name = "Price", ResourceType = typeof(Resources.OrderingDetail))]
    public decimal? Price { get; set; }

    [Display(Name = "Quantity", ResourceType = typeof(Resources.OrderingDetail))]
    public long? Quantity { get; set; }

    [Display(Name = "Note", ResourceType = typeof(Resources.OrderingDetail))]
    public string Note { get; set; }

    [Display(Name = "Link", ResourceType = typeof(Resources.OrderingDetail))]
    public string Link { get; set; }

    [Display(Name = "LinkUrl", ResourceType = typeof(Resources.OrderingDetail))]
    public string LinkUrl { get; set; }

    [Display(Name = "Color", ResourceType = typeof(Resources.OrderingDetail))]
    public string Color { get; set; }

    [Display(Name = "Size", ResourceType = typeof(Resources.OrderingDetail))]
    public string Size { get; set; }

    [Display(Name = "OrderingId", ResourceType = typeof(Resources.Order))]
    public Guid OrderingId { get; set; }
  }
}