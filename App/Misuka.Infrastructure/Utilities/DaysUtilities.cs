using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Infrastructure.Utilities
{
  public class DaysUtilities
  {
    public static double DaysLeave(DateTime fromDate, DateTime toDate)
    {
      double day = 0;
      var tmpDate = fromDate;
      while (tmpDate.AddDays(1) <= toDate)
      {
        day += 1;
        tmpDate = tmpDate.AddDays(1);
      }
      double hour = 0;
      if (fromDate.Hour > toDate.Hour)
      {
        hour = (fromDate.Hour + (double)(fromDate.Minute / 60)) - (toDate.Hour + (double)(toDate.Minute / 60));

      }
      else
      {
        hour = (toDate.Hour + (double)(toDate.Minute / 60)) - (fromDate.Hour + (double)(fromDate.Minute / 60));

      }
      if (hour != 0)
      {
        if (hour > 4)
        {
          day += 1;
        }
        else
        {
          day += 0.5;
        }
      }
    
      return day;
    }
  }
}
