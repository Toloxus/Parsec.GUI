using Serilog;
using System;
using System.IO;

namespace Parsec.Core.Logging
{
    public class LoggingService : ILoggingService
    {
        public LoggingService(string logFilePath)
        {
            if (File.Exists(logFilePath))
                File.Delete(logFilePath);

            var logConfiguration = new LoggerConfiguration()
                                           .MinimumLevel.Verbose()
                                           .WriteTo.Console()
                                           .WriteTo.File(logFilePath);
            Log.Logger = logConfiguration.CreateLogger();
        }

        public void Debug(Type caller, string message, Exception exception = null)
        {
            Log.Logger.Debug(exception, $"{caller.Name} | {message}");
        }

        public void Debug(object caller, string message, Exception exception = null)
        {
            Debug(caller.GetType(), message, exception);
        }

        public void Error(Type caller, string message, Exception exception = null)
        {
            Log.Logger.Error(exception, $"{caller.Name} | {message}");
        }

        public void Error(object caller, string message, Exception exception = null)
        {
            Error(caller.GetType(), message, exception);
        }

        public void Fatal(Type caller, string message, Exception exception = null)
        {
            Log.Logger.Fatal(exception, $"{caller.Name} | {message}");
        }

        public void Fatal(object caller, string message, Exception exception = null)
        {
            Fatal(caller.GetType(), message, exception);
        }

        public void Info(Type caller, string message, Exception exception = null)
        {
            Log.Logger.Information(exception, $"{caller.Name} | {message}");
        }

        public void Info(object caller, string message, Exception exception = null)
        {
            Info(caller.GetType(), message, exception);
        }

        public void Verbose(Type caller, string message, Exception exception = null)
        {
            Log.Logger.Verbose(exception, $"{caller.Name} | {message}");
        }

        public void Verbose(object caller, string message, Exception exception = null)
        {
            Verbose(caller.GetType(), message, exception);
        }

        public void Warn(Type caller, string message, Exception exception = null)
        {
            Log.Logger.Warning(exception, $"{caller.Name} | {message}");
        }

        public void Warn(object caller, string message, Exception exception = null)
        {
            Warn(caller.GetType(), message, exception);
        }
    }
}
