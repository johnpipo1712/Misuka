using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.SearchCriteria
{
    public class OrderingSearchCriteria
    {
       public string Keyword { get; set; }
       public Guid? PersonId { get; set; }
       public int? Status { get; set; }
       public DateTime? FromDate { get; set; }
       public DateTime? ToDate { get; set; }


       public object IsDownPayment { get; set; }
    }
}
