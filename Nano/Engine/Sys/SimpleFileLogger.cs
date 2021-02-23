using System;
using System.IO;

namespace Nano.Engine.Sys
{
    public class SimpleFileLogger : ILoggingService , IDisposable
    {
        private StreamWriter m_Stream;

        public SimpleFileLogger(string filePath,bool append)
        {
            m_Stream = new StreamWriter(filePath, append);
            m_Stream.AutoFlush = true;
        }

        #region IDisposable implementation

        public void Log(string message, LogLevel logLevel = LogLevel.Info)
        {
            string outputString = string.Format("{0} : {1} : {2}", 
                DateTime.Now,
                logLevel.ToString(), 
                message 
                );
            m_Stream.WriteLine(outputString);
        }

        public void Dispose()
        {
            if(m_Stream != null)
            {
                m_Stream.Close();
                m_Stream = null;
            }
        }

        #endregion
    }
}

