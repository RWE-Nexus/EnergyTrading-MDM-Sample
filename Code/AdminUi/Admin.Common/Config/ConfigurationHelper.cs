namespace Common.Config
{
    using System.Configuration;

    public class ConfigurationHelper
    {
        public static bool SendBusNotificationMessages
        {
            get
            {
                var config = ConfigurationManager.AppSettings["SendBusNotificationMessages"];

                return string.IsNullOrEmpty(config) || bool.Parse(config);
            }
        }
    }
}