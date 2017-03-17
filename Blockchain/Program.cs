using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Blockchain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServerPort = GetCommandlineArgummentValue("-serverPort", args) ?? "5001";
            var url = "http://localhost:" + ServerPort;   

            var host = new WebHostBuilder()                
                .UseKestrel()
                .UseUrls(url)
                .UseContentRoot(Directory.GetCurrentDirectory())
                /*.UseIISIntegration()*/
                .UseStartup<Startup>()                
                .Build();

            host.Run();
        }

        public static string ServerPort;

        private static string GetCommandlineArgummentValue(string key, string[] args)
        {
            key = key + ":";
            foreach(var arg in args)
            {
                if (arg.StartsWith(key))
                {
                    return arg.Substring(key.Length);
                }
            }

            return null;
        }
    }

    
}
