using System;

namespace Nano.Engine.Sys
{
    public class NullLogger : ILoggingService
    {
        public NullLogger()
        {
        }

        #region ILoggingService implementation

        public void Log(string message, LogLevel logLevel = LogLevel.Info)
        {
            //Intentionally drop all log messages
        }

        #endregion
    }
}

