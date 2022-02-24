using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var defaultConnString = "Server=(LocalDb)\\.;Database=Rookie.Ecom.MetaShop;Trusted_Connection=True;";
            if (args.Contains("/seed"))
            {
                SeedData.EnsureSeedData(defaultConnString);
            }
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
