using System;

namespace Parsec.Core.Logging
{
    /// <summary>
    /// Wrapper around Serilog library.
    /// </summary>
    public interface ILoggingService
    {
        void Verbose(Type caller, string message, Exception exception = null);
        void Debug(Type caller, string message, Exception exception = null);
        void Info(Type caller, string message, Exception exception = null);
        void Warn(Type caller, string message, Exception exception = null);
        void Fatal(Type caller, string message, Exception exception = null);
        void Error(Type caller, string message, Exception exception = null);

        void Verbose(object caller, string message, Exception exception = null);
        void Debug(object caller, string message, Exception exception = null);
        void Info(object caller, string message, Exception exception = null);
        void Warn(object caller, string message, Exception exception = null);
        void Fatal(object caller, string message, Exception exception = null);
        void Error(object caller, string message, Exception exception = null);
    }
}
