namespace Common.Services
{
    using System.Configuration;

    public static class Server
    {
        public static string Name;

        public static void Set(string something)
        {
            if (string.IsNullOrWhiteSpace(something))
            {
                //use default
                Name = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["default_service"]];
                return;
            }

            if (something.StartsWith("http"))
            {
                //server name
                Name = something;
                return;
            }

            if (something.StartsWith("service_"))
            {
                //server id
                Name = ConfigurationManager.AppSettings[something];
                return;
            }

            //assume server id without service_
            Name = ConfigurationManager.AppSettings["service_" + something];
        }
    }
}