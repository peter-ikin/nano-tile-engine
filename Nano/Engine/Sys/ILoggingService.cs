using System;

namespace Nano.Engine.Sys
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }

    public interface ILoggingService
    {
        void Log(string message, LogLevel logLevel);
    }
}

