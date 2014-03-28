namespace Shell
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Markup;

    using Common.Services;

    using Microsoft.Practices.EnterpriseLibrary.Logging;
    using Microsoft.Practices.ServiceLocation;

    using Shell.Services;

    public partial class App : Application
    {
        private static Bootstrapper bootstrapper;

        static App()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement), 
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Server.Set(e.Args.Length > 0 ? e.Args[0] : null);
            if (e.Args.Length > 4)
            {
                WindowPosition.SavePosition(
                    double.Parse(e.Args[1]), 
                    double.Parse(e.Args[2]), 
                    double.Parse(e.Args[3]), 
                    double.Parse(e.Args[4]));
            }

#if (DEBUG)
            RunInDebugMode();
#else
            RunInReleaseMode();
#endif
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            LogWriter logger;

            try
            {
                logger = ServiceLocator.Current.GetInstance<LogWriter>();
                logger.Write(ex);
                MessageBox.Show("Unhandled Exception - Application must now close: " + ex.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("Application Initialisation Failed: " + e.Message + "\n" + ex.Message);
            }

            Environment.Exit(1);
        }

        private static void RunInDebugMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            try
            {
                bootstrapper = new Bootstrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            try
            {
                bootstrapper = new Bootstrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}