
using System;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository;
using Misuka.Infrastructure;

namespace Misuka.Infrastructure.Logs
{
    public class _Log : ILog
    {
        public static readonly _Log Instance = new _Log();
        private bool _initialized;

        private _Log()
        {
        }

        public void Initialize()
        {
            if (_initialized) return;
            XmlConfigurator.Configure();

            _initialized = true;
        }

        public Guid Debug(object sender, string message)
        {
            if (!_initialized) return Guid.Empty;
            var referenceId = Guid.NewGuid();
            GetLogger(sender).Debug(message);
            return referenceId;
        }

        public Guid Info(object sender, string message)
        {
            if (!_initialized) return Guid.Empty;
            var referenceId = Guid.NewGuid();
            LogicalThreadContext.Properties["CustomerReferenceId"] = referenceId;
            GetLogger(sender).Info(message);
            return referenceId;
        }

        public Guid Error(object sender, string message)
        {
            if (!_initialized) return Guid.Empty;
            var referenceId = Guid.NewGuid();
            LogicalThreadContext.Properties["CustomerReferenceId"] = referenceId;
            GetLogger(sender).Error(message);
            return referenceId;
        }

        public Guid Warning(object sender, string message)
        {
            if (!_initialized) return Guid.Empty;
            var referenceId = Guid.NewGuid();
            LogicalThreadContext.Properties["CustomerReferenceId"] = referenceId;
            GetLogger(sender).Warn(message);
            return referenceId;
        }

        public Guid UnhandledException(object sender, Exception exception, string userId)
        {
            if (!_initialized) return Guid.Empty;
            var referenceId = Guid.NewGuid();
            LogicalThreadContext.Properties["CustomerReferenceId"] = referenceId;
            var sb = new StringBuilder();

            sb.AppendLine("Unhandled Exception: ");
            sb.AppendFormat("User ID: {0} | ", userId);
            if (exception.InnerException != null)
            {
                sb.AppendFormat("Inner Exception Type: {0} | ", exception.InnerException.GetType());
                sb.AppendFormat("Inner Exception Message: {0} | ", exception.InnerException.Message);
                sb.AppendFormat("Inner Exception Source: {0} | ", exception.InnerException.Source);
                if (exception.InnerException.StackTrace != null)
                    sb.AppendFormat("Inner Exception StackTrace: {0} | ", exception.InnerException.StackTrace);
            }
            sb.AppendFormat("Exception Type: {0} | ", exception.GetType());
            sb.AppendFormat("Exception Message: {0} | ", exception.Message);
            sb.AppendFormat("Exception Source: {0} | ", exception.Source);
            if (exception.StackTrace != null)
                sb.AppendFormat("Exception Stack Trace: {0}", exception.StackTrace);

            GetLogger(sender).Error(sb.ToString());
            return referenceId;
        }

        public void Flush()
        {
            FlushLog();
        }

        private log4net.ILog GetLogger(object sender)
        {
            return GetLogger(sender.GetType());
        }

        private log4net.ILog GetLogger(Type type)
        {
            return GetLogger(type.FullName);
        }

        private log4net.ILog GetLogger(string typeName)
        {
            var log = LogManager.GetLogger(typeName);
            return log;
        }

        internal void FlushLog()
        {
            ILoggerRepository loggerRepository = LogManager.GetRepository();
            foreach (var appender in loggerRepository.GetAppenders())
            {
                var buffered = appender as BufferingAppenderSkeleton;
                if(buffered != null)
                {
                    buffered.Flush();
                }
            }
        }
    }
}