﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Infrastructure.Cofiguration
{
    public class ApplicationSettings
    {
        public static string GetPropertyValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}