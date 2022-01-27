using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Service.Utility.Helper
{
    public class StaticContextHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static IConfigurationBuilder Getbuilder()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");
            return builder;
        }

        public static string GetAppSetting(string key)
        {
            //return Convert.ToString(ConfigurationManager.AppSettings[key]);
            var builder = Getbuilder();
            var GetAppStringData = builder.Build().GetValue<string>(key);
            return GetAppStringData;
        }
        public static HttpContext Current => _httpContextAccessor.HttpContext;
    }
}
