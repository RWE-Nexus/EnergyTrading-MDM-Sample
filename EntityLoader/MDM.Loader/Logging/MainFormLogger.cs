namespace MDM.Loader.Logging
{
    using System;
    using System.Threading;
    using MDM.Loader.Views;
    using EnergyTrading.Logging;
    using EnergyTrading.Logging.Log4Net;

    public class MainFormLogger : ILogger
    {
        private readonly IMainFormView mainFormView;
        private readonly ILogger log4netLogger = new Log4NetLoggerFactory(new Log4NetConfiguration()).GetLogger(typeof(FormMain));

        public MainFormLogger(IMainFormView mainFormView)
        {
            this.mainFormView = mainFormView;
        }

        public void Debug(string message)
        {
            Log(message, m => log4netLogger.Debug(m));
        }

        public void Debug(string message, Exception exception)
        {
            Log(message, exception, (m, e) => log4netLogger.Debug(m, e));
        }

        public void DebugFormat(string format, params object[] parameters)
        {
            Log(string.Format(format, parameters), m => log4netLogger.Debug(m));
        }

        public void Info(string message)
        {
            Log(message, m => log4netLogger.Info(m));
        }

        public void Info(string message, Exception exception)
        {
            Log(message, exception, (m, e) => log4netLogger.Info(m, e));
        }

        public void InfoFormat(string format, params object[] parameters)
        {
            Log(string.Format(format, parameters), m => log4netLogger.Info(m));
        }

        public void Warn(string message)
        {
            Log(message, m => log4netLogger.Warn(m));
        }

        public void Warn(string message, Exception exception)
        {
            Log(message, exception, (m, e) => log4netLogger.Warn(m, e));
        }

        public void WarnFormat(string format, params object[] parameters)
        {
            Log(string.Format(format, parameters), m => log4netLogger.Warn(m));
        }

        public void Error(string message)
        {
            Log(message, m => log4netLogger.Error(m));
        }

        public void Error(string message, Exception exception)
        {
            Log(message, exception, (m, e) => log4netLogger.Error(m, e));
        }

        public void ErrorFormat(string format, params object[] parameters)
        {
            Log(string.Format(format, parameters), m => log4netLogger.Error(m));
        }

        public void Fatal(string message)
        {
            Log(message, m => log4netLogger.Fatal(m));
        }

        public void Fatal(string message, Exception exception)
        {
            Log(message, exception, (m, e) => log4netLogger.Fatal(m, e));
        }

        public void FatalFormat(string format, params object[] parameters)
        {
            Log(string.Format(format, parameters), m => log4netLogger.Fatal(m));
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

        private void Log(string message, Action<string> action)
        {
            LogToView(message);
            action(message);
            Thread.Sleep(1);
        }

        private void Log(string message, Exception exception, Action<string, Exception> logAction)
        {
            LogToView(message);
            logAction(message, exception);
            Thread.Sleep(1);
        }

        private void LogToView(string message)
        {
            this.mainFormView.SetStatusMesage(message);
            this.mainFormView.AppendLogText(message);
        }
    }
}