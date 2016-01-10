using System;

namespace Misuka.Infrastructure.Utilities
{
  public class DateTimeUtilities
  {
    public static DateTime GetCurrentBusinessDate()
    {
      var curHour = DateTime.Now.Hour;
      if (curHour <= 2)//02:00 AM
        return DateTime.Now.Date.AddDays(-1);
      return DateTime.Now.Date;
    }
    public static bool CheckSuAndSa(DateTime fromDate,DateTime toDate)
    {
      var result = false;
      for (var i = fromDate; i <= toDate; i = i.AddDays(1))
      {
        if (i.DayOfWeek == DayOfWeek.Sunday || i.DayOfWeek == DayOfWeek.Saturday)
        {
          result = true;
          break;
        }
      }
      return result;
    }
    public static DateTime GetToDayLeaves(DateTime fromDate, double leavedays)
    {
      var todate = fromDate;
      if (leavedays >= 1)
      {
        var days = (int)leavedays / 1;
        var hours = leavedays % 1;
        todate = todate.AddDays(days);
        if (hours > 0)
        {
          todate = todate.AddHours(4);
         
        }
     
      }
      else
      {
        todate = todate.AddHours(4);
      }
      if (todate.Hour >= 12 && fromDate.Hour <= 12)
      {
        todate = todate.AddHours(1);
      }
      else if (todate.Hour <= 9)
      {
        todate = todate.AddDays(-1);
        todate = new DateTime(todate.Year, todate.Month, todate.Day, 18, 0, 0);
      }
       
      return todate;
    }
  }
}
