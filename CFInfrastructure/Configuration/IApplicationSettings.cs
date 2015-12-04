using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace CFInfrastructure.Configuration
{
    public class ApplicationSettings
    {
        public static string GetPropertyValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
