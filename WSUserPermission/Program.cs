using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using WSUserPermission.Utils;

namespace WSUserPermission
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            var logRoute = ConfigurationManager.AppSetting["logRoute"];
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .WriteTo.Console()
              .WriteTo.File(logRoute, rollingInterval: RollingInterval.Day)
              .CreateLogger();


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
