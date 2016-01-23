using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Misuka.Domain.Entity
{
  public class OrderingHistory : Misuka.Infrastructure.EntityFramework.Entity
  {
    public Guid OrderingHistoryId { get; set; }

    public Guid? OrderingId { get; set; }

    public int? Type { get; set; }

    public Guid? ActionBy { get; set; }

    public string ActionByNane { get; set; }

    public DateTime? ActionDate { get; set; }

    public int Status { get; set; }
  }
  public class OrderingHistoryMap : EntityTypeConfiguration<OrderingHistory>
  {
    public OrderingHistoryMap()
    {
      this.HasKey(t => t.OrderingHistoryId);
      this.ToTable("[dbo].[OrderingHistory]");
    }
  }
}
