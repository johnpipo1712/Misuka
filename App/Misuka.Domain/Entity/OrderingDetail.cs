using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.Entity
{

  public class OrderingDetail : Misuka.Infrastructure.EntityFramework.Entity
  {
    public Guid OrderingDetailId { get; set; }

    public string OrderingDetailCode { get; set; }

    public Guid OrderingId { get; set; }

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
  public class OrderingDetailMap : EntityTypeConfiguration<OrderingDetail>
  {
    public OrderingDetailMap()
    {
      this.HasKey(t => t.OrderingDetailId);
      this.ToTable("[dbo].[OrderingDetail]");
    }
  }
}