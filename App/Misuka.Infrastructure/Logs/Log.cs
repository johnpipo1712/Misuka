using System;

namespace Misuka.Infrastructure.Logs
{
  public static class Log
  {
    public static void Initialize()
    {
      _Log.Instance.Initialize();
    }

    public static Guid Debug(object sender, string message)
    {
      return _Log.Instance.Debug(sender, message);
    }

    public static Guid Info(object sender, string message)
    {
      return _Log.Instance.Info(sender, message);
    }

    public static Guid Warning(object sender, string message)
    {
      return _Log.Instance.Warning(sender, message);
    }

    public static Guid Error(object sender, string message)
    {
      return _Log.Instance.Error(sender, message);
    }

    public static Guid UnhandledException(object sender, Exception exception, string userId)
    {
      return _Log.Instance.UnhandledException(sender, exception, userId);
    }

    public static void FlushLog()
    {
      _Log.Instance.FlushLog();
    }
  }
}