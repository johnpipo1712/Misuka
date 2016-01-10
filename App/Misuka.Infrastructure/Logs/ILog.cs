using System;

namespace Misuka.Infrastructure.Logs
{
  public interface ILog
  {
    Guid Debug(object sender, string message);
    Guid Info(object sender, string message);
    Guid Error(object sender, string message);
    Guid Warning(object sender, string message);
    Guid UnhandledException(object sender, Exception exception, string userId);
    void Flush(); 
  }
}