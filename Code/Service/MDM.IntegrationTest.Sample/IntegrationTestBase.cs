namespace EnergyTrading.MDM.Test
{
    using System.Collections.Generic;

    public class IntegrationTestBase
    {
        protected static string DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.fffffffZ";

        protected static ObjectScript Script
        {
            get
            {
                return SetUpFixture.Script;
            }
        }

        protected static Dictionary<string, string> ServiceUrl
        {
            get
            {
                return SetUpFixture.ServiceUrl;
            }
        }
    }
}