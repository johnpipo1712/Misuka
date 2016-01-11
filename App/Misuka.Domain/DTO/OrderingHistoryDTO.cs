using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.DTO
{
  public class OrderingHistoryDTO
  {
    public Guid OrderingHistoryId { get; set; }

    public Guid? OrderingId { get; set; }

    public int? Type { get; set; }

    public Guid? ActionBy { get; set; }

    public string ActionByNane { get; set; }

    public DateTime? ActionDate { get; set; }
  }
}
