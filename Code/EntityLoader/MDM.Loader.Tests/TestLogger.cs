namespace MDM.Loader.Tests
{
    using System;

    using EnergyTrading.Logging;

    public class TestLogger : ILogger
    {
        public string Error { get; private set; }
        public string Info { get; private set; }
        public string Warn { get; private set; }
        public string Debug { get; private set; }

        void ILogger.Debug(string message)
        {
            Debug = message;
        }

        void ILogger.Debug(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(string format, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        void ILogger.Info(string message)
        {
            Info = message;
        }

        void ILogger.Info(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void InfoFormat(string format, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        void ILogger.Warn(string message)
        {
            Warn = message;
        }

        void ILogger.Warn(string message, Exception exception)
        {
            
        }

        public void WarnFormat(string format, params object[] parameters)
        {
            
        }

        void ILogger.Error(string message)
        {
            Error = message;
        }

        void ILogger.Error(string message, Exception exception)
        {
            
        }

        public void ErrorFormat(string format, params object[] parameters)
        {
            Error = string.Format(format, parameters);
        }

        public void Fatal(string message)
        {
            
        }

        public void Fatal(string message, Exception exception)
        {
            
        }

        public void FatalFormat(string format, params object[] parameters)
        {
            
        }

        public bool IsDebugEnabled
        {
            get { return true; }
        }

        public bool IsInfoEnabled
        {
            get { return true; }
        }

        public bool IsWarnEnabled
        {
            get { return true; }
        }

        public bool IsErrorEnabled
        {
            get { return true; }
        }

        public bool IsFatalEnabled
        {
            get { return true; }
        }
    }
}