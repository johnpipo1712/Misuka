using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class OrderingHistoryModel
  {
    public Guid OrderingHistoryId { get; set; }

    public Guid? OrderingId { get; set; }

    public int? Type { get; set; }

    public Guid? ActionBy { get; set; }

    public string ActionByNane { get; set; }

    public DateTime? ActionDate { get; set; }
  }
}