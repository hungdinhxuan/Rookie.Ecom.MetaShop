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
            var defaultConnString = "Server=.;Database=Rookie.Ecom.MetaShop;User Id=sa;Password=Strong!Passw0rd;";
            if (args.Contains("/seed"))
            {
                SeedData.EnsureSeedData(defaultConnString);
            }
            var builder = CreateHostBuilder(args).Build();
            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
