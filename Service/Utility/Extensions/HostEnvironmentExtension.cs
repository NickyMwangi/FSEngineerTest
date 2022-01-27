using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Service.Utility.Extensions
{
    public static class HostEnvironmentExtension
    {
        public static string MapPath(this IWebHostEnvironment environment, string path)
        {
            var result = path ?? string.Empty;
            if (environment.IsPathMapped(path) == false)
            {
                var wwwroot = environment.WwwRoot();
                if (result.StartsWith("~", StringComparison.Ordinal))
                {
                    result = result.Substring(1);
                }
                if (result.StartsWith("/", StringComparison.Ordinal))
                {
                    result = result.Substring(1);
                }
                result = Path.Combine(wwwroot, result.Replace('/', '\\'));
            }

            return result;
        }

        public static string UnmapPath(this IWebHostEnvironment environment, string path)
        {
            var result = path ?? string.Empty;
            if (environment.IsPathMapped(path))
            {
                var wwwroot = environment.WwwRoot();
                result = result.Remove(0, wwwroot.Length);
                result = result.Replace('\\', '/');

                var prefix = (result.StartsWith("/", StringComparison.Ordinal) ? "~" : "~/");
                result = prefix + result;
            }

            return result;
        }

        public static bool IsPathMapped(this IWebHostEnvironment environment, string path)
        {
            var result = path ?? string.Empty;
            return result.StartsWith(environment.WebRootPath, StringComparison.Ordinal);
        }

        public static string WwwRoot(this IWebHostEnvironment environment)
        {
            // todo: take it from project.json!!!
            var result = Path.Combine(environment.WebRootPath, "CoreTicket");
            return result;
        }
    }
}
