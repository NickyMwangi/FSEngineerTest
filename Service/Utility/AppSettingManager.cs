using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Service.Utility
{
    public static class AppSettingManager
    {
        public static IConfiguration AppSetting { get; }
        static AppSettingManager()
        {
            AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        }
    }
}
