namespace MDM.Loader
{
    using System;
    using System.Configuration;
    using System.Windows.Forms;

    using MDM.Loader.Configuration;
    using MDM.Loader.Logging;
    using Microsoft.Practices.Unity;

    using EnergyTrading.Console;
    using EnergyTrading.Logging;
    using EnergyTrading.Logging.Log4Net;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log4net.GlobalContext.Properties["LogFileName"] = string.Format("MDMLoaderLog-{0}.csv", DateTime.Now.ToString("yyyyMMdd-HHmmssfff"));
            LoggerFactory.SetProvider(() => new Log4NetLoggerFactory(new Log4NetConfiguration()));
            LoggerFactory.Initialize();

            var logger = LoggerFactory.GetLogger("MDM Loader");

            logger.Debug("MDM Loader initialisation...");

            var args = new MDMLoaderCommandLineArgs();
            args.Initialize();
            if (!string.IsNullOrWhiteSpace(args.MdmServiceUri))
            {
                logger.InfoFormat("MDM Service Uri overridden by command line parameter to: {0}", args.MdmServiceUri);
                ConfigurationManager.AppSettings["MdmUri"] = args.MdmServiceUri;
            }
            var container = MDMLoaderConfiguration.Configure();

            logger.WarnFormat("MDM Uri:{0}", ConfigurationManager.AppSettings["MdmUri"]);
            logger.InfoFormat("EntityName:{0}; FilePath:{1}; IsInUIMode:{2}; HasUnhandledArguments:{3};Candidate Set:{4}", args.EntityName, args.FilePath, args.IsInUiMode.ToString(),
                args.HasUnhandledParameters.ToString(), args.CandidateData);
            

            var isInUiMode = System.Diagnostics.Debugger.IsAttached || 
                             args.IsInUiMode || 
                             (!args.HasUnhandledParameters && string.IsNullOrWhiteSpace(args.EntityName));

            if (!isInUiMode && !args.HasUserConfirmed)
            {
                Console.WriteLine("Please verify the above information and press \"y\" to continue, otherwise press any key to exit.");
                if (Console.ReadKey().Key != ConsoleKey.Y)
                {
                    return;
                }
            }

            if (isInUiMode)
            {
                ConsoleUtils.HideConsoleWindow();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var form = new FormMain();
                // Change the logging provider, to set UI to display the log messages
                LoggerFactory.SetProvider(() => new SimpleLoggerFactory(new MainFormLogger(form)));
                Application.Run(form);
            }
            else
            {
                logger.InfoFormat("Command Line:{0}", Environment.CommandLine);

                var service = container.Resolve<IMDMDataLoaderService>();
                service.Load(args.EntityName, args.FilePath, args.CandidateData, canStopLoadProcessorOnLoadComplete: true);

                // Console.WriteLine("Load process has completed, press any key to exit.");
                // Console.ReadKey();
            }
        }
    }
}